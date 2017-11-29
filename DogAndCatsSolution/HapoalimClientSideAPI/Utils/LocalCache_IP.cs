using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HapoalimAPI.Utils
{
    public class IPHolder
    {
        public long? IP_ID { get; set; }
        public string IP { get; set; }
        public DateTime ExpDT { get; set; }
        public int Count = 0;
        public int Max = 100;

        public IPHolder(long? ipid, string ip, int count, int max, DateTime expAt)
        {
            IP_ID = ipid;
            IP = ip;
            Max = max;
            Count = count;
            ExpDT = new DateTime(expAt.Ticks);

        }
        public int Add()
        {
            Count++;
            return Count;
        }

        public bool Update(long IPID, int new_max, int new_count, DateTime new_ExpDate)
        {
            if (IPID > this.IP_ID.GetValueOrDefault())
            {
                IP_ID = IPID;
                Count = new_count;
                ExpDT = new DateTime(new_ExpDate.Ticks);
                Max = new_max;
            }
            else
            {
                if (new_count > Count) Count = new_count;

                if (new_ExpDate > ExpDT) ExpDT = new DateTime(new_ExpDate.Ticks);

                if (new_max > Max) Max = new_max;
            }
            return true;
        }

        public bool need_Captcha()
        {

            return DateTime.UtcNow < ExpDT && Count > Max;
        }

        public void Clone(IPHolder toclone)
        {
            IP_ID = toclone.IP_ID;
            IP = toclone.IP;
            ExpDT = new DateTime(toclone.ExpDT.Ticks);
            Count = toclone.Count;
            Max = toclone.Max;
        }
    }
    public static class Cache_IP
    {

        private static Guid CacheGuid = Guid.NewGuid();

        private static volatile Dictionary<string, IPHolder> IPs = new Dictionary<string, IPHolder>();

        private static DateTime LastClear = new DateTime(DateTime.UtcNow.Ticks),
                                NowException = new DateTime(DateTime.UtcNow.Ticks);

        private static object locker1 = new object(), locker2 = new object();

        private static bool Update()
        {
            if (DateTime.UtcNow.Date > LastClear.Date)
            {
                lock (locker1)
                {
                    if (DateTime.UtcNow.Date > LastClear.Date)
                    {
                        lock (locker2)
                        {
                            if (DateTime.UtcNow.Date > LastClear.Date)
                            {

                                IPs = new Dictionary<string, IPHolder>();
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// IF NOT EXIST ADD NEW, ELSE UPDTAE EXISTING AND RETURN EXISTING
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="ip"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IPHolder AddUpdate_IP(string IP, IPHolder ip, string phone, string code, out string exception)
        {
            exception = string.Empty;
            if (DateTime.UtcNow.Date > LastClear.Date)
            {
                Update();
            }

            try
            {
                lock (locker1)
                {
                    if (!IPs.ContainsKey(IP))
                    {
                        IPs.Add(IP, ip);

                    }
                    else
                    {
                        IPHolder current = IPs[IP];
                        current.Update(ip.IP_ID.GetValueOrDefault(), ip.Max, ip.Count, ip.ExpDT);
                        IPs[IP] = current;
                        ip.Clone(current);
                    }
                }

            }
            catch (Exception ex)
            {
                exception = UtilsException.GetMSG(ref ex);
                NowException = new DateTime(DateTime.UtcNow.Ticks);
                #region MyRegion LOG

                //UtilsDB.API_Log_Insert(enumProjectType.Holidays, enumAction.LocalCache, enumLogType._1_Exception, enumLogType._1_Exception,
                //                                             "AddUpdate_IP",
                //                                             IP,
                //                                             exception,
                //                                             true,
                //                                             "",
                //                                             "",
                //                                             "",
                //                                             ref NowException,
                //                                             ref NowException,
                //                                             ref CacheGuid, false, phone);

                #endregion
            }
            return ip;
        }

        private static object getLocker = new object();
        /// <summary>
        /// ADD IF NOT EXIST
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static IPHolder IP_Get(string IP, int ToAddIfNeeded, string phone, string code, out string exception, ref DateTime apicreatedate_ISR)
        {
            exception = string.Empty;
            IPHolder current = null;
            if (DateTime.UtcNow.Date > LastClear.Date)
            {
                Update();
            }

            try
            {

                if (IPs.ContainsKey(IP))
                {
                    if (ToAddIfNeeded > 0)
                    {
                        lock (getLocker)
                        {
                            lock (locker1)
                            {
                                IPHolder cur = IPs[IP];
                                cur.Count++;
                                IPs[IP] = cur;
                            }
                        }
                    }
                    return IPs[IP];
                }
                else
                {
                    lock (getLocker)
                    {

                        //current = new IPHolder(null, IP, ToAddIfNeeded, HolidaysManager.IP_AllowedPerCycle, new DateTime(DateTime.UtcNow.Ticks).AddMinutes(HolidaysManager.IP_MinuteCycle));

                        current = AddUpdate_IP(IP, current, phone, code, out exception);


                    }
                }

            }
            catch (Exception ex)
            {
                exception = UtilsException.GetMSG(ref ex);
                #region MyRegion LOG

                //UtilsDB.API_Log_Insert(enumProjectType.Holidays, enumAction.LocalCache, enumLogType._1_Exception, enumLogType._1_Exception,
                //                                             "IP_Get",
                //                                             "CODE: " + code,
                //                                             exception,
                //                                             true,
                //                                             "",
                //                                             IP,
                //                                             "",
                //                                             ref apicreatedate_ISR,
                //                                             ref apicreatedate_ISR,
                //                                             ref CacheGuid, true, phone);

                #endregion
            }
            return current;

        }
    }
}