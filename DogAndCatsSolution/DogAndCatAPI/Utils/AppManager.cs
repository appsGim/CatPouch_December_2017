using DogAndCatAPI.DAL;
using DogAndCatAPI.Models;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DogAndCatAPI.Utils
{



    public static class AppManager
    {
        private static Guid AppManagerGuid = Guid.NewGuid();
        private static object lockDate = new object();
        private static DateTime lastUIGet = new DateTime(DateTime.UtcNow.AddDays(-1).Ticks);
        private static DateTime Last_Config_Update_ISR = new DateTime(UtilsDateTime.UTC_To_Israel_Time().AddDays(-1).Ticks);
        public static bool cleanhelper()
        {
            lastUIGet = new DateTime(DateTime.UtcNow.AddDays(-1).Ticks);
            Last_Config_Update_ISR = new DateTime(DateTime.UtcNow.AddDays(-2).Ticks);
            return true;
        }
         


        private static volatile int ConfigUpdateAsMinutes = int.Parse(ConfigurationManager.AppSettings["ConfigUpdateAsMinutes"].ToString()),
                                    ClientFetchDataInterval_AsMinutes = int.Parse(ConfigurationManager.AppSettings["ClientFetchDataInterval_AsMinutes"].ToString());

        public static volatile int IP_MinuteCycle = int.Parse(ConfigurationManager.AppSettings["IP_MinuteCycle"].ToString()),
                                   IP_AllowedPerCycle = int.Parse(ConfigurationManager.AppSettings["IP_AllowedPerCycle"].ToString()),

                                   Unique_MinuteCycle = int.Parse(ConfigurationManager.AppSettings["Unique_MinuteCycle"].ToString()),
                                   Unique_AllowedPerCycle = int.Parse(ConfigurationManager.AppSettings["Unique_AllowedPerCycle"].ToString()),
                                   Local_Cache_Refresh_Hour_Interval= int.Parse(ConfigurationManager.AppSettings["Local_Cache_Refresh_Hour_Interval"].ToString());
                                 



        public static DateTime //Dog_StartDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Dog_StartDate"].ToString(), "yyyy-MM-dd", null),
                               //Dog_EndDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Dog_EndDate"].ToString(), "yyyy-MM-dd", null),
                               Cat_StartDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Cat_StartDate"].ToString(), "yyyy-MM-dd", null),
                               Cat_EndDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Cat_EndDate"].ToString(), "yyyy-MM-dd", null);


        

        public static Guid //Dog_ServerToken = Guid.Parse(ConfigurationManager.AppSettings["Dog_ServerToken"].ToString()),
                           Cat_ServerToken= Guid.Parse(ConfigurationManager.AppSettings["Cat_ServerToken"].ToString());


        private static bool RenewProjectToken = ConfigurationManager.AppSettings["RenewProjectToken"].ToString().Equals("true");
        public static bool RestrictServerIP = ConfigurationManager.AppSettings["RestrictServerIP"].ToString().Equals("true");


        //private static volatile List<API_Age_GetResult> Ages = new List<API_Age_GetResult>();
        private static volatile List<UIOut> UIResponse = new List<UIOut>();
     //   private static volatile List<PetOut> Pets = new List<PetOut>();
        private static volatile List<API_Project_GetResult> Projects = new List<API_Project_GetResult>();
        //private static volatile List<API_Location_GetResult> Locations = new List<API_Location_GetResult>();

        public static API_Project_GetResult GetProject(enumProject Project)
        {
            if (Last_Config_Update_ISR.AddMinutes(ConfigUpdateAsMinutes) < UtilsDateTime.UTC_To_Israel_Time())
            {
                Update();
            }

            return (from p in Projects where p.ID == (int)Project select p).FirstOrDefault();
        } 

        public static Dictionary<string, dynamic> get_data_helper(DateTime ISRTime)
        {
            if (Last_Config_Update_ISR.AddHours(ConfigUpdateAsMinutes) < ISRTime || UIResponse.Count == 0)
            {
                Update();
            }
              
            return new Dictionary<string, dynamic>()
            {
                {"Dog_UI",(from p in UIResponse where p.ProjectID==1 select p).ToList() },
                {"Cat_UI",(from p in UIResponse where p.ProjectID==2 select p).ToList() },
               // { "Dog_Pets", (from p in Pets where p.ProjectID==1 select p).ToList()},
               // { "Cat_Pets", (from p in Pets where p.ProjectID==2 select p).ToList()},
                { "Dog_Token",(from p in Projects where p.ID==1 select p.Token).FirstOrDefault()},
                 { "Cat_Token",(from p in Projects where p.ID==1 select p.Token).FirstOrDefault()}
            };
        }

        public static volatile List<string> AllowedDomains = ConfigurationManager.AppSettings["AllowedDomains"].ToString().Split(',').ToList();

        private static object locker1 = new object(), locker2 = new object(), lockToken1 = new object(), lockToken2 = new object();

        /// <summary>
        /// TO BE USED AT IS ALIVE
        /// </summary>
        /// <param name="ISRDate"></param>
        /// <param name="Project"></param>
        /// <param name="APITransaction"></param>
        /// <param name="CurrentToken"></param>
        /// <returns></returns>
        public static bool CampaignAlive(DateTime ISRDate, enumProject Project, ref Guid APITransaction, out Guid CurrentToken)
        {
            if (Last_Config_Update_ISR.AddMinutes(ConfigUpdateAsMinutes) < UtilsDateTime.UTC_To_Israel_Time())
            {
                Update();
            }

            CurrentToken = APITransaction;

            API_Project_GetResult project = null;

            if (Projects.Count > 0)
            {
                project = (from p in Projects where p.ID == (int)Project && p.StartDate <= ISRDate && p.EndIncludeDate >= ISRDate.Date && p.Enabled select p).FirstOrDefault();

                if (project != null)
                {
                    CurrentToken = project.Token;
                    return true;
                }
            }

            return false;

            //if (Project == enumProject.Dog)
            //{ 
            //    return Dog_StartDate.Date <= ISRDate.Date && Dog_EndDate.Date >= ISRDate.Date;
            //}

            //return Cat_StartDate.Date <= ISRDate.Date && Cat_EndDate.Date>= ISRDate.Date ;
        }

        /// <summary>
        /// TO BE USED AT LEAD INSERT
        /// </summary>
        /// <param name="ISRDate"></param>
        /// <param name="Project"></param>
        /// <param name="APITransaction"></param>
        /// <param name="TokenToValidate"></param>
        /// <param name="TokenError"></param>
        /// <returns></returns>
        public static bool CampaignAlive(DateTime ISRDate, enumProject Project, ref Guid APITransaction,  
                                         Guid TokenToValidate,out bool TokenError)
        {
            if (Last_Config_Update_ISR.AddMinutes(ConfigUpdateAsMinutes) < UtilsDateTime.UTC_To_Israel_Time())
            {
                Update();
            }

            TokenError = true;

            API_Project_GetResult project = null;

            if (Projects.Count > 0)
            {
                project = (from p in Projects where p.ID == (int)Project && p.StartDate <= ISRDate && p.EndIncludeDate >= ISRDate.Date && p.Enabled 
                           && (p.OldToken== TokenToValidate || p.Token==TokenToValidate) select p).FirstOrDefault();

                if (project != null)
                {
                    TokenError = false;
                    return true;
                }
            }

            return false;

            //if (Project == enumProject.Dog)
            //{ 
            //    return Dog_StartDate.Date <= ISRDate.Date && Dog_EndDate.Date >= ISRDate.Date;
            //}

            //return Cat_StartDate.Date <= ISRDate.Date && Cat_EndDate.Date>= ISRDate.Date ;
        }


        public static bool Update()
        {
            DateTime ISRTime = UtilsDateTime.UTC_To_Israel_Time();
            DateTime UTCNow =new DateTime ( DateTime.UtcNow.Ticks);
            
            if (Last_Config_Update_ISR.AddMinutes(ConfigUpdateAsMinutes) < ISRTime)
            {
                lock (locker1)
                {
                    if (Last_Config_Update_ISR.AddMinutes(ConfigUpdateAsMinutes) < ISRTime)
                    {
                        lock (locker2)
                        {
                            if (Last_Config_Update_ISR.AddMinutes(ConfigUpdateAsMinutes) < ISRTime)
                            {

                                if (ConfigurationManager.AppSettings["AZURE"].Equals("true"))
                                {
                                    RenewProjectToken = RoleEnvironment.GetConfigurationSettingValue("RenewProjectToken").ToString().Equals("true");
                                }
                                else
                                {
                                    RenewProjectToken = ConfigurationManager.AppSettings["RenewProjectToken"].ToString().Equals("true");
                                }

                                    

                                List<API_UI_UserResponse_GetResult>   UIResponse_DB = UtilsDB.API_UI_UserResponse_Get(AppManagerGuid, ISRTime);
                                Projects = UtilsDB.API_Project_Get(AppManagerGuid, ISRTime, RenewProjectToken);

                                List<UIOut> UIResponse_TEMP = new List<UIOut>();
                                foreach (API_UI_UserResponse_GetResult UI in UIResponse_DB)
                                {
                                    UIResponse_TEMP.Add(new Models.UIOut() { ID = UI.ID, MSG = UI.Text, ProjectID = UI.ProjectID });
                                }
                                UIResponse = UIResponse_TEMP;
                                
                                #region MyRegion CONFIGURATION

                                if (ConfigurationManager.AppSettings["AZURE"].Equals("true"))
                                {
                                    AllowedDomains = RoleEnvironment.GetConfigurationSettingValue("AllowedDomains").ToString().Split(',').ToList();
                                    RestrictServerIP = RoleEnvironment.GetConfigurationSettingValue("RestrictServerIP").ToString().Equals("true");
                                   // Dog_StartDate = DateTime.ParseExact(RoleEnvironment.GetConfigurationSettingValue("Dog_StartDate").ToString(), "yyyy-MM-dd", null);
                                   // Dog_EndDate = DateTime.ParseExact(RoleEnvironment.GetConfigurationSettingValue("Dog_EndDate").ToString(), "yyyy-MM-dd", null);
                                    Cat_StartDate = DateTime.ParseExact(RoleEnvironment.GetConfigurationSettingValue("Cat_StartDate").ToString(), "yyyy-MM-dd", null);
                                    Cat_EndDate = DateTime.ParseExact(RoleEnvironment.GetConfigurationSettingValue("Cat_EndDate").ToString(), "yyyy-MM-dd", null);
                                     
                                    ConfigUpdateAsMinutes = int.Parse(RoleEnvironment.GetConfigurationSettingValue("ConfigUpdateAsMinutes").ToString());
                                    ClientFetchDataInterval_AsMinutes= int.Parse(RoleEnvironment.GetConfigurationSettingValue("ClientFetchDataInterval_AsMinutes").ToString());

                                   // Dog_ServerToken = Guid.Parse(RoleEnvironment.GetConfigurationSettingValue("Dog_ServerToken").ToString());
                                    Cat_ServerToken = Guid.Parse(RoleEnvironment.GetConfigurationSettingValue("Cat_ServerToken").ToString()); 

                                    IP_MinuteCycle = int.Parse(RoleEnvironment.GetConfigurationSettingValue("IP_MinuteCycle").ToString());
                                    IP_AllowedPerCycle = int.Parse(RoleEnvironment.GetConfigurationSettingValue("IP_AllowedPerCycle").ToString());

                                    Unique_MinuteCycle = int.Parse(RoleEnvironment.GetConfigurationSettingValue("Unique_MinuteCycle").ToString());
                                    Unique_AllowedPerCycle = int.Parse(RoleEnvironment.GetConfigurationSettingValue("Unique_AllowedPerCycle").ToString());
                                    Local_Cache_Refresh_Hour_Interval = int.Parse(RoleEnvironment.GetConfigurationSettingValue("Local_Cache_Refresh_Hour_Interval").ToString());
                                }
                                else
                                {
                                    RenewProjectToken = ConfigurationManager.AppSettings["RenewProjectToken"].ToString().Equals("true");
                                    RestrictServerIP = ConfigurationManager.AppSettings["RestrictServerIP"].ToString().Equals("true");
                                    AllowedDomains = ConfigurationManager.AppSettings["AllowedDomains"].ToString().Split(',').ToList();
                                    Local_Cache_Refresh_Hour_Interval = int.Parse(ConfigurationManager.AppSettings["Local_Cache_Refresh_Hour_Interval"].ToString());
                                  //  Dog_StartDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Dog_StartDate"].ToString(), "yyyy-MM-dd", null);
                                  //  Dog_EndDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Dog_EndDate"].ToString(), "yyyy-MM-dd", null);
                                    Cat_StartDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Cat_StartDate"].ToString(), "yyyy-MM-dd", null);
                                    Cat_EndDate = DateTime.ParseExact(ConfigurationManager.AppSettings["Cat_EndDate"].ToString(), "yyyy-MM-dd", null);

                                    ConfigUpdateAsMinutes = int.Parse(ConfigurationManager.AppSettings["ConfigUpdateAsMinutes"].ToString());
                                    ClientFetchDataInterval_AsMinutes = int.Parse(ConfigurationManager.AppSettings["ClientFetchDataInterval_AsMinutes"].ToString());

                                  //  Dog_ServerToken = Guid.Parse(ConfigurationManager.AppSettings["Dog_ServerToken"].ToString());
                                    Cat_ServerToken = Guid.Parse(ConfigurationManager.AppSettings["Cat_ServerToken"].ToString()); 

                                    IP_MinuteCycle = int.Parse(ConfigurationManager.AppSettings["IP_MinuteCycle"].ToString());
                                    IP_AllowedPerCycle = int.Parse(ConfigurationManager.AppSettings["IP_AllowedPerCycle"].ToString());

                                    Unique_MinuteCycle = int.Parse(ConfigurationManager.AppSettings["Unique_MinuteCycle"].ToString());
                                    Unique_AllowedPerCycle = int.Parse(ConfigurationManager.AppSettings["Unique_AllowedPerCycle"].ToString());
                                     
                                }

                                #endregion

                                Last_Config_Update_ISR = new DateTime(ISRTime.Ticks);

                            }
                            
                        }
                    }

                }

            }

            return true;
        }
         
        
        public static List<UIOut> UIResponse_Get( ref DateTime ISRTime,  enumProject Project)
        {
            if (Last_Config_Update_ISR.AddHours(ConfigUpdateAsMinutes) < ISRTime || UIResponse.Count == 0)
            {
                Update();
            }

            if (lastUIGet.AddMinutes(ClientFetchDataInterval_AsMinutes) < DateTime.UtcNow)
            {
                lastUIGet = new DateTime(DateTime.UtcNow.Ticks);

                return (from ui in UIResponse where ui.ProjectID == (int)Project select ui).ToList();
            }

            return new List<Models.UIOut>();
            
        }

        #region MyRegion PET REMOVED
        /// <summary>
        /// SHOULD BE CALLED ONLY IF WE SEND UIOut
        /// </summary>
        /// <param name="ISRTime"></param>
        /// <param name="Project"></param>
        /// <returns></returns>
        //public static List<PetOut> PetProjectGet(ref DateTime ISRTime, enumProject Project)
        //{
        //    if (Last_Config_Update_ISR.AddHours(ConfigUpdateAsMinutes) < ISRTime || UIResponse.Count == 0)
        //    {
        //        Update();
        //    }

        //    return (from ui in Pets where ui.ProjectID == (int)Project select ui).ToList();

        //}

        //public static PetOut PetGet(ref DateTime ISRTime, Guid Pet, enumProject Project)
        //{
        //    if (Last_Config_Update_ISR.AddHours(ConfigUpdateAsMinutes) < ISRTime || UIResponse.Count == 0)
        //    {
        //        Update();
        //    }

        //    return (from p in Pets where p.ProjectID == (int)Project && (p.TOld==Pet || p.T==Pet) select p).FirstOrDefault();
        //} 
        #endregion
    }

    public static class Domains
    {
        private static volatile List<string> AllowedDomains = null;

        public static List<string> Get_AllowedDomains()
        {
            if (AllowedDomains == null)
            {
                if (!ConfigurationManager.AppSettings["AZURE"].Equals("true"))
                {

                    AllowedDomains = ConfigurationManager.AppSettings["AllowedDomains"].Split(',').ToList();
                }
                else
                {

                    AllowedDomains = RoleEnvironment.GetConfigurationSettingValue("AllowedDomains").Split(',').ToList();
                }
            }

            return AllowedDomains;
        }
    }
}