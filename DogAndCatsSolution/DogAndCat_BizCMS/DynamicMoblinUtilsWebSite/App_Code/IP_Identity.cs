using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System;

public class IP_IdentityMetaData
{
    [Display(Order =1)]
    [FilterUIHint("Int64_Range")]
    public long ID;

    [Display(Order = 2)]
    [FilterUIHint("Date")]
    public DateTime InsertDate;
    
    [Display(Order = 3)]
    [FilterUIHint("Search")]
    public string IP;
 
    [Display(Order = 7)]
    [FilterUIHint("Int_Range")]
    public int Count;
}

[MetadataType(typeof(IP_IdentityMetaData))]
public partial class IP_Identity
{
   
}