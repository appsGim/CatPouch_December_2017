using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System;

public class LogMetaData
{
    [Display(Order =1)]
    [FilterUIHint("Int64_Range")]
    public long ID;

    [Display(Order = 2)]
    [FilterUIHint("Date")]
    public DateTime InsertDate;

    [Display(Order = 3)]
    [FilterUIHint("Search")]
    public string MSG;

    [Display(Order = 4)]
    [FilterUIHint("Search")]
    public string MSG2;

    [Display(Order = 5)]
    [FilterUIHint("Search")]
    public string Error;

    [Display(Order = 6)]
    [FilterUIHint("SearchGuid")]
    public Guid APITransaction;

    [Display(Order = 7)]
    [FilterUIHint("Search")]
    public string IP;
}

[MetadataType(typeof(LogMetaData))]
public partial class Log
{
   
}