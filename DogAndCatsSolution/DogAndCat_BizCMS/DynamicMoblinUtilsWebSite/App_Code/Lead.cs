using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System;

public class LeadMetaData
{
    [Display(Order = 1)]
    [FilterUIHint("Int64_Range")]
    public long ID;

    [Display(Order = 2)]
    [FilterUIHint("Date")]
    public DateTime InsertDateISR;

    [Display(Order = 3)]
    [FilterUIHint("SearchGuid")]
    public string APITransaction;

    [Display(Order = 4)]
    [FilterUIHint("Search")]
    public string Email;

    [Display(Order = 5)]
    [FilterUIHint("Search")]
    public string Phone;

    [Display(Order = 6)]
    [FilterUIHint("Search")]
    public string FName;

    [Display(Order = 7)]
    [FilterUIHint("Search")]
    public string LName;

    [Display(Order = 8)]
    [FilterUIHint("Int64_Range")]
    public long UniqueUserID;

   
    [Display(Order = 10)]
    [FilterUIHint("Search")]
    public string IP;
    
}

[MetadataType(typeof(LeadMetaData))]
public partial class Lead
{
   
}