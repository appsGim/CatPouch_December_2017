using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
 
using System.Net;
using DogAndCatAPI.Models;
using System.Data.Linq;
using DogAndCatAPI.DAL;

namespace DogAndCatAPI.Utils
{
    public static class UtilsDB
    {
        private static string _Connection { get; set; }

        public static string Connection()
        {
            if (_Connection == null) _Connection = ConfigurationManager.ConnectionStrings["DogAndCatDB"].ConnectionString;
            return _Connection;
        }
         
        public static bool API_Log_Insert(enumAction ActionType, enumLogType LogType, enumLogType InnerLogType,
                                                          string MSG, string MSG2, string ERROR, bool Critical,
                                                          string refferer, string ip, string ua,
                                                          ref DateTime CreateDate,
                                                          ref DateTime CreateDateISR, ref Guid APITransaction, bool useq, string identifier,enumProject Project
                                                           )
        {

            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    db.API_Log_Insert((int)ActionType, (int)LogType, (int)InnerLogType, MSG, MSG2, ERROR, Critical,
                                                         refferer, ip, ua,
                                                         CreateDate, CreateDateISR, APITransaction, "" , identifier,(int)Project);
                }
                return true;

            }
            catch (Exception ex1)
            {
                try
                {
                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        db.API_Log_Insert((int)ActionType, (int)LogType, (int)InnerLogType, MSG, MSG2, ERROR, Critical,
                                                         refferer, ip, ua,
                                                         CreateDate, CreateDateISR, APITransaction, "", identifier, (int)Project);
                    }
                    return true;
                }
                catch (Exception ex2)
                {

                }
            }
            return false;
        }

   
        public static API_Unique_Identity_AddUpdateResult API_Unique_Identity_AddUpdate(string Unique,
                                                                       ref DateTime CreateDate,
                                                                       ref DateTime CreateDateISR,
                                                                       ref Guid APITransaction, ref enumAction Action,ref enumProject Project)
        {

            API_Unique_Identity_AddUpdateResult result = null;
            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    result = db.API_Unique_Identity_AddUpdate(APITransaction, Unique, CreateDate,
                        CreateDateISR, AppManager.Unique_AllowedPerCycle, AppManager.Unique_MinuteCycle, (int)Project).FirstOrDefault();
                }
                return result;

            }

            catch (Exception ex1)
            {
                try
                {
                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        result = db.API_Unique_Identity_AddUpdate(APITransaction, Unique, CreateDate,
                                                                   CreateDateISR, AppManager.Unique_AllowedPerCycle, 
                                                                   AppManager.Unique_MinuteCycle, (int)Project).FirstOrDefault();
                    }
                    return result;

                }
                catch (Exception ex2)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(Action, enumLogType._1_DBException, enumLogType._1_DBException,
                                                                 null,
                                                                 "API_Unique_Identity_AddUpdate",
                                                                 UtilsException.GetMSG(ref ex2),
                                                                 true,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 ref CreateDate,
                                                                 ref CreateDateISR,
                                                                 ref APITransaction, false, Unique,Project);

                    #endregion
                }
            }
            return result;
        }
 



        public static API_Lead_InsertResult API_Lead_Insert(ref SubmitLead req, int PetType,
                                                                          ref Guid APITransaction, 
                                                                          string Captch,
                                                                          int AddToIP,
                                                                          ref DateTime CreateDate,
                                                                          ref DateTime CreateDateISR,
                                                                          ref enumPlatformType PlatformType,
                                                                          ref enumProject Project)
        {

            API_Lead_InsertResult result = null;
            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    result = db.API_Lead_Insert(APITransaction,req.Email,(int)PlatformType, req.IP, req.UA,CreateDate,CreateDateISR,null,
                                                         Captch,
                                                         AppManager.Unique_AllowedPerCycle, AppManager.Unique_MinuteCycle,
                                                       (int)Project,req.FName,req.LName,req.Email,req.Phone,req.City,req.Street,req.STNumber,req.FlatNumber,req.POBox,
                                                       req.Regulation,req.AcceptContent, null/* PetType*/,/*req.PetBDay*/null,
                                                       AppManager.IP_AllowedPerCycle,AppManager.IP_MinuteCycle, AddToIP).FirstOrDefault();
                }
                return result;

            }

            catch (Exception ex1)
            {
                try
                {

                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        result = db.API_Lead_Insert(APITransaction, req.Email, (int)PlatformType, req.IP, req.UA, CreateDate, CreateDateISR, null,
                                                         Captch,
                                                         AppManager.Unique_AllowedPerCycle, AppManager.Unique_MinuteCycle,
                                                       (int)Project, req.FName, req.LName, req.Email, req.Phone, req.City, req.Street, req.STNumber, req.FlatNumber, req.POBox,
                                                       req.Regulation, req.AcceptContent,null/* PetType*/, /*req.PetBDay*/null,
                                                       AppManager.IP_AllowedPerCycle, AppManager.IP_MinuteCycle, AddToIP).FirstOrDefault();
                    }
                    return result;

                }
                catch (Exception ex2)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(enumAction.Lead, enumLogType._1_DBException, enumLogType._1_DBException,
                                                                 Newtonsoft.Json.JsonConvert.SerializeObject(req),
                                                                 "API_Lead_Insert_Offer",
                                                                 UtilsException.GetMSG(ref ex2),
                                                                 true,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 ref CreateDate,
                                                                 ref CreateDateISR,
                                                                 ref APITransaction, false, req.Email, Project);

                    #endregion
                }
            }
            return result;
        }



        public static List<API_UI_UserResponse_GetResult> API_UI_UserResponse_Get(Guid APITransaction, DateTime CreateDate)
        {

          List<  API_UI_UserResponse_GetResult> result = null;
            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    result = db.API_UI_UserResponse_Get().ToList();
                }
                return result;

            }

            catch (Exception ex1)
            {
                try
                {

                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        result = db.API_UI_UserResponse_Get().ToList();
                    }
                    return result;

                }
                catch (Exception ex2)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(enumAction.Lead, enumLogType._1_DBException, enumLogType._1_DBException,
                                                                 null,
                                                                 "API_UI_UserResponse_Get",
                                                                 UtilsException.GetMSG(ref ex2),
                                                                 true,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 ref CreateDate,
                                                                 ref CreateDate,
                                                                 ref APITransaction, false, null, enumProject.APIGeneral);

                    #endregion
                }
            }
            return result;
        }

        public static List<API_Project_GetResult> API_Project_Get(Guid APITransaction, DateTime CreateDate, bool RenewToken)
        {

            List<API_Project_GetResult> result = null;
            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    result = db.API_Project_Get(RenewToken).ToList();
                }
                return result;

            }

            catch (Exception ex1)
            {
                try
                {

                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        result = db.API_Project_Get(RenewToken).ToList();
                    }
                    return result;

                }
                catch (Exception ex2)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(enumAction.Lead, enumLogType._1_DBException, enumLogType._1_DBException,
                                                                 null,
                                                                 "API_Project_Get -> RenewToken: "+ RenewToken.ToString(),
                                                                 UtilsException.GetMSG(ref ex2),
                                                                 true,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 ref CreateDate,
                                                                 ref CreateDate,
                                                                 ref APITransaction, false, null, enumProject.APIGeneral);

                    #endregion
                }
            }
            return result;
        }


        #region MyRegion PET REMOVED - PREDEFINED PET ONLY
        //public static List<API_Pet_GetResult> API_Pet_Get(Guid APITransaction, DateTime CreateDate, bool RenewToken)
        //{

        //    List<API_Pet_GetResult> result = null;
        //    try
        //    {
        //        using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
        //        {
        //            result = db.API_Pet_Get(RenewToken).ToList();
        //        }
        //        return result;

        //    }

        //    catch (Exception ex1)
        //    {
        //        try
        //        {

        //            using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
        //            {
        //                result = db.API_Pet_Get(RenewToken).ToList();
        //            }
        //            return result;

        //        }
        //        catch (Exception ex2)
        //        {
        //            #region MyRegion LOG

        //            UtilsDB.API_Log_Insert(enumAction.Lead, enumLogType._1_DBException, enumLogType._1_DBException,
        //                                                         null,
        //                                                         "API_Pet_Get -> RenewToken: "+ RenewToken.ToString(),
        //                                                         UtilsException.GetMSG(ref ex2),
        //                                                         true,
        //                                                         null,
        //                                                         null,
        //                                                         null,
        //                                                         ref CreateDate,
        //                                                         ref CreateDate,
        //                                                         ref APITransaction, false, null, enumProject.APIGeneral);

        //            #endregion
        //        }
        //    }
        //    return result;
        //} 
        #endregion
        public static API_Lead_Update_CouponMSGResult API_Lead_Update_CouponMSG(ref Guid APITransaction, ref DateTime CreateDate,
                                                                                ref DateTime CreateDateISR, long leadid, string reponseMSGCoupon, bool success)
        {

            API_Lead_Update_CouponMSGResult result = null;
            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    result = db.API_Lead_Update_CouponMSG(leadid, reponseMSGCoupon, success).FirstOrDefault();
                }
                return result;

            }

            catch (Exception ex1)
            {
                try
                {

                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        result = db.API_Lead_Update_CouponMSG(leadid, reponseMSGCoupon, success).FirstOrDefault();
                    }
                    return result;

                }
                catch (Exception ex2)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(enumAction.Lead, enumLogType._1_DBException, enumLogType._1_DBException,
                                                                 reponseMSGCoupon,
                                                                 "API_Lead_Update_CouponMSG",
                                                                 UtilsException.GetMSG(ref ex2),
                                                                 true,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 ref CreateDate,
                                                                 ref CreateDateISR,
                                                                 ref APITransaction, false, null, enumProject.Dog);

                    #endregion
                }
            }
            return result;
        }


        public static API_Validate_Registered_UserResult API_Validate_Registered_User(Guid APITransaction, DateTime CreateDateISR, string email, enumProject Project)
        {

            API_Validate_Registered_UserResult   result = null;
            try
            {
                using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                {
                    result = db.API_Validate_Registered_User((int)Project, email).FirstOrDefault();
                }
                return result;

            }

            catch (Exception ex1)
            {
                try
                {

                    using (DogAndCatDBDataContext db = new DogAndCatDBDataContext(Connection()) { CommandTimeout = 120 })
                    {
                        result = db.API_Validate_Registered_User((int)Project, email).FirstOrDefault();
                    }
                    return result;

                }
                catch (Exception ex2)
                {
                    #region MyRegion LOG

                    UtilsDB.API_Log_Insert(enumAction.Lead, enumLogType._1_DBException, enumLogType._1_DBException,
                                                                 null,
                                                                 "API_Validate_Registered_User",
                                                                 UtilsException.GetMSG(ref ex2),
                                                                 true,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 ref CreateDateISR,
                                                                 ref CreateDateISR,
                                                                 ref APITransaction, false, email, Project);

                    #endregion
                }
            }
            return result;
        }
    }
}