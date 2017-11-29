using System;
using System.Threading;
using Microsoft.WindowsAzure;
//using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System.Configuration;
using Microsoft.WindowsAzure.StorageClient;

namespace DogAndCatAPI.Utils.Queues
{
    //public class q_log
    //{
    //    public Guid api_transaction { get; set; }
    //    public DateTime api_create_date { get; set; }
    //    public DateTime api_create_date_isr { get; set; }
    //    public int project_type { get; set; }
    //    public int action_type { get; set; }
    //    public int log_type { get; set; }
    //    public int inner_log_type { get; set; }
    //    public string msg { get; set; }
    //    public string msg2 { get; set; }
    //    public bool critical { get; set; }
    //    public string error { get; set; }
    //    public string refferer { get; set; }
    //    public string ip { get; set; }
    //    public string ua { get; set; }

    //    public string instance_id { get; set; }
    //    public string identifier { get; set; }

    //    public q_log() { }
    //}
    //public enum enumQ { QLog }
    //public static class Queue_Handler
    //{
    //    private static Guid Queue_HandlerGuid = Guid.NewGuid();

    //    #region REGION STORAGE AND CLOUD

    //    #region REGION GENERAL: CREADENTIALS / STORAGE ACCOUNT / QUEUE CLIIENT

    //    private static StorageCredentialsAccountAndKey CredentialsAccount;
    //    private static CloudStorageAccount Cloud_StorageAccount;
    //    private static CloudQueueClient Cloud_QueueClient;

    //    #endregion

    //    #region REGION CLOUD QUEUE TYPES

    //    private static CloudQueue Cloud_QLog;// { get; set; } 

    //    #endregion

    //    #endregion

    //    #region REGION STORAGE ACCOUNT: BLOB/QUEUE/TABLE RELATED TO STORAGE - PROPERTIES

    //    private static string StorageAccountName, StorageKey, QueueUri, TableUri, BlobUri,


    //                                                Q_Name_QLog;

    //    #endregion

    //    /// <summary>
    //    /// LOCKER OBJECT AT UPDATE PROCESS
    //    /// </summary>
    //    private static object UpdateLocker = new object();

    //    /// <summary>
    //    /// MAIN BOOLEAN TO KNOW IF UPDATE DONE
    //    /// </summary>
    //    private static bool UpdatedQueueHandler = false;

    //    //public static bool ApplicationStart_Update()
    //    //{
    //    //	Update();
    //    //	return true;
    //    //}
    //    /// <summary>
    //    /// ACTUAL UPDATE IF UPDATE NEVER HAPPENED
    //    /// </summary>
    //    private static void Update()
    //    {
    //        if (!UpdatedQueueHandler)
    //        {
    //            lock (UpdateLocker)
    //            {
    //                if (!UpdatedQueueHandler)
    //                {
    //                    #region MyRegion MAIN Q
    //                    try
    //                    {
    //                        #region BASE Q

    //                        StorageAccountName = ConfigurationManager.AppSettings["Storage_AccountName"].ToString();
    //                        StorageKey = ConfigurationManager.AppSettings["Storage_AccountKey"].ToString();
    //                        QueueUri = ConfigurationManager.AppSettings["Storage_QueueUri"].ToString();
    //                        TableUri = ConfigurationManager.AppSettings["Storage_TableUri"].ToString();
    //                        BlobUri = ConfigurationManager.AppSettings["Storage_BlobUri"].ToString();
    //                        Q_Name_QLog = ConfigurationManager.AppSettings["Storage_QLog"].ToString();

    //                        CredentialsAccount = new StorageCredentialsAccountAndKey(StorageAccountName, StorageKey);

    //                        Cloud_StorageAccount = new CloudStorageAccount(CredentialsAccount,
    //                                                                                         new Uri(BlobUri),
    //                                                                                         new Uri(QueueUri),
    //                                                                                         new Uri(TableUri));

    //                        Cloud_QueueClient = Cloud_StorageAccount.CreateCloudQueueClient();

    //                        Cloud_QLog = Cloud_QueueClient.GetQueueReference(Q_Name_QLog);
    //                        bool created = Cloud_QLog.CreateIfNotExist();

    //                        UpdatedQueueHandler = true;

    //                        #endregion
    //                    }
    //                    catch (Exception ex)
    //                    {

    //                        DateTime dt = UtilsDateTime.UTC_To_Israel_Time();

    //                        //UtilsDB.API_Log_Insert(enumProjectType.Holidays, enumAction.QHandler, enumLogType._1_Exception, enumLogType._1_Exception,
    //                        //                                 UtilsException.GetMSG(ref ex),
    //                        //                                 null,
    //                        //                                 null,
    //                        //                                 false,
    //                        //                                 null,
    //                        //                                 null,
    //                        //                                 null,
    //                        //                                 ref dt,
    //                        //                                 ref dt,
    //                        //                                 ref Queue_HandlerGuid, false, "Q Handler");


    //                        UpdatedQueueHandler = false;
    //                    }

    //                    #endregion
    //                }
    //            }
    //        }
    //    }

    //    //////////////////////////////////////////////////////////// API API API API API API API API ////////////////////////////////////////////////////////////
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="q"></param>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public static bool Add_MSG_To_Q( enumQ q, object obj, ref Guid APITransaction, ref DateTime APICreateDate, string identifier)
    //    {
    //        int h = 0;

    //        if (!UpdatedQueueHandler) Update();

    //        try
    //        {
    //            switch (q)
    //            {
    //                case enumQ.QLog:
    //                    #region QLOG
    //                    try
    //                    {

    //                        Cloud_QLog.BeginAddMessage(new CloudQueueMessage(JsonConvert.SerializeObject(obj)), null, null);

    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        DateTime dt = new DateTime(DateTime.UtcNow.Ticks);
    //                        Thread.Sleep(25);
    //                        try
    //                        {
    //                            Cloud_QLog.BeginAddMessage(new CloudQueueMessage(JsonConvert.SerializeObject(obj)), null, null);
    //                        }
    //                        catch (Exception ex2)
    //                        {

    //                            #region MyRegion LOG

    //                            //UtilsDB.API_Log_Insert(ProjectType, enumAction.QHandler, enumLogType._1_Exception, enumLogType._1_Exception,
    //                            //                                                 "90 EX2",
    //                            //                                                 null,
    //                            //                                                 UtilsException.GetMSG(ref ex2),
    //                            //                                                 true,
    //                            //                                                 "API",
    //                            //                                                 "",
    //                            //                                                 "",
    //                            //                                                 ref dt,
    //                            //                                                 ref dt,
    //                            //                                                 ref Queue_HandlerGuid, false, null);

    //                            #endregion

    //                            return false;
    //                        }
    //                    }
    //                    #endregion
    //                    break;
    //            }

    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            DateTime dt = new DateTime(DateTime.UtcNow.Ticks);

    //            #region MyRegion LOG

    //            UtilsDB.API_Log_Insert( enumAction.QHandler, enumLogType._1_Exception, enumLogType._1_Exception,
    //                                                             "100 EX WRAPPER ALL",
    //                                                             null,
    //                                                             UtilsException.GetMSG(ref ex),
    //                                                             true,
    //                                                             "API",
    //                                                             "",
    //                                                             "",
    //                                                             ref dt,
    //                                                             ref dt,
    //                                                             ref Queue_HandlerGuid, false, identifier);

    //            #endregion

    //        }
    //        return false;
    //    }
    //}
}