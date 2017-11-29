using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System;

public class LeadReportMetaData
{
    [Display(Order = 1)]
    [FilterUIHint("Int64_Range")]
    public long RID;

    [Display(Order = 2)]
    [FilterUIHint("Date")]
    public DateTime InsertDateISR;

    [Display(Order = 3)]
    [FilterUIHint("Search")]
    public string FName;

    [Display(Order = 4)]
    [FilterUIHint("Search")]
    public string LName;

    [Display(Order = 5)]
    [FilterUIHint("Search")]
    public string   City;

    [Display(Order = 6)]
    [FilterUIHint("Search")]
    public string Street;

    [Display(Order = 7)]
    [FilterUIHint("Search")]
    public string Email;

    [Display(Order = 8)]
    [FilterUIHint("Search")]
    public string Phone;

    //[Display(Order = 9)]
    //[FilterUIHint("Search")]
    //public string Pet;

    [Display(Order = 10)]
    [FilterUIHint("Int_Range")]
    public int ProjectID;
}

[MetadataType(typeof(LeadReportMetaData))]
public partial class LeadReport
{
   
}