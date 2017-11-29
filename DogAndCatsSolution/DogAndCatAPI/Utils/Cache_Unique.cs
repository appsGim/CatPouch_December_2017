using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DogAndCatAPI.Utils
{

    public static class Cache_Unique
    {

        private static volatile int Local_Cache_Refresh_Hour_Interval = 3;

        private static Guid CacheGuid = Guid.NewGuid();

        private static volatile Dictionary<string, DateTime> UNIQUE_DICTIONARY = new Dictionary<string, DateTime>();

        private static DateTime LastClear_UTC = new DateTime(DateTime.UtcNow.AddDays(-1).Ticks);

        private static object locker1 = new object(), locker2 = new object();

        public static bool helperclean()
        {

            LastClear_UTC = new DateTime(DateTime.UtcNow.Ticks);
            UNIQUE_DICTIONARY = new Dictionary<string, DateTime>();
            return UNIQUE_DICTIONARY.Count == 0;
        }

        private static bool Update()
        {
            if (DateTime.UtcNow > LastClear_UTC.AddHours(AppManager.Local_Cache_Refresh_Hour_Interval))
            {
                lock (locker1)
                {
                    if (DateTime.UtcNow > LastClear_UTC.AddHours(AppManager.Local_Cache_Refresh_Hour_Interval))
                    {
                        lock (locker2)
                        {
                            if (DateTime.UtcNow > LastClear_UTC.AddHours(AppManager.Local_Cache_Refresh_Hour_Interval))
                            {
                                UNIQUE_DICTIONARY = new Dictionary<string, DateTime>(); 
                                LastClear_UTC = new DateTime(DateTime.UtcNow.Ticks);

                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ADD OR UPDATE THE MAC AND EXPIRED TIME
        /// </summary>
        /// <param name="UNIQUE"></param>
        /// <param name="expdate"></param>
        /// <returns></returns>
        public static bool ADDREJECT_UNIQUE(ref DateTime APICreateDate, string UNIQUE, DateTime expdate, ref enumProject Project)
        {
            if (DateTime.UtcNow > LastClear_UTC.AddHours(Local_Cache_Refresh_Hour_Interval))
            {
                Update();
            }

            try
            {

                if (!UNIQUE_DICTIONARY.ContainsKey(((int)Project).ToString()+"_"+UNIQUE))
                {
                    lock (locker1)
                    {
                        UNIQUE_DICTIONARY.Add(((int)Project).ToString() + "_" + UNIQUE, expdate);
                    }
                    return true;
                }
                else
                {
                    lock (locker1)
                    {
                        UNIQUE_DICTIONARY[((int)Project).ToString() + "_" + UNIQUE] = expdate;
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                #region MyRegion LOG

                UtilsDB.API_Log_Insert(enumAction.LocalCache, enumLogType._1_Exception, enumLogType._1_Exception,
                                                             "ADDREJECT_MAC_UNIQUE ERROR",
                                                             UNIQUE,
                                                             UtilsException.GetMSG(ref ex),
                                                             true,
                                                             "",
                                                             "",
                                                             "",
                                                             ref APICreateDate,
                                                             ref APICreateDate,
                                                             ref CacheGuid, true, UNIQUE,   Project);

                #endregion
            }

            return false;
        }

        /// <summary>
        /// CHECK IF EXIST AND EXIRATION TIME
        /// </summary>
        /// <param name="APICreateDate"></param>
        /// <param name="UNIQUE"></param>
        /// <returns></returns>
        public static bool MAC_UNIQUE_IS_EXIST(ref DateTime APICreateDate, string UNIQUE, ref enumProject Project)
        {
            if (APICreateDate > LastClear_UTC.AddHours(Local_Cache_Refresh_Hour_Interval))
            {
                Update();
            }
            try
            {

                if (!UNIQUE_DICTIONARY.ContainsKey(((int)Project).ToString() + "_" + UNIQUE)) return false;

                return UNIQUE_DICTIONARY[((int)Project).ToString() + "_" + UNIQUE] > APICreateDate;

            }
            catch (Exception ex)
            { 
                #region MyRegion LOG

                UtilsDB.API_Log_Insert(enumAction.LocalCache, enumLogType._1_Exception, enumLogType._1_Exception,
                                                             "MAC_OR_UNIQUE Exist ERROR",
                                                             UNIQUE,
                                                             UtilsException.GetMSG(ref ex),
                                                             true,
                                                             "",
                                                             "",
                                                             "",
                                                             ref APICreateDate,
                                                             ref APICreateDate,
                                                             ref CacheGuid, true, UNIQUE,Project);

                #endregion
            }
            return false;

        }

        public static bool REMOVE_MAC_UNIQUE(ref DateTime APICreateDate, string UNIQUE, ref enumProject Project)
        {
            if (APICreateDate > LastClear_UTC.AddHours(Local_Cache_Refresh_Hour_Interval))
            {
                Update();
            }

            if (UNIQUE_DICTIONARY.ContainsKey(((int)Project).ToString() + "_" + UNIQUE))
            {
                try
                {

                    UNIQUE_DICTIONARY.Remove(((int)Project).ToString() + "_" + UNIQUE);
                    return true;
                }
                catch (Exception ex)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(enumAction.LocalCache, enumLogType._1_Exception, enumLogType._1_Exception,
                                                                 "Remove MAC_OR_UNIQUE ERROR",
                                                                 UNIQUE,
                                                                 UtilsException.GetMSG(ref ex),
                                                                 true,
                                                                 "",
                                                                 "",
                                                                 "",
                                                                 ref APICreateDate,
                                                                 ref APICreateDate,
                                                                 ref CacheGuid, true, UNIQUE,Project);

                    #endregion

                    return false;
                }
            }
            return true;

        }
    }

}