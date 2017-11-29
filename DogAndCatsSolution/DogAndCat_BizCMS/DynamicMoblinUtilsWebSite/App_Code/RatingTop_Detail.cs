using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System;

public class RatingTop_DetailMetaData
{

    [Display(Order = 2)]
    [FilterUIHint("Int_Range")]
    public int CityID;
    [Display(Order = 3)]
    [FilterUIHint("Int_Range")]
    public int NeighbourhoodID;
    [Display(Order = 4)]
    [FilterUIHint("Int_Range")]
    public int CategoryID;
    [Display(Order = 5)]
    [FilterUIHint("Int_Range")]
    public int BusinessID;


    [Display(Order = 7)]
    [FilterUIHint("Search")]
    public string CItyName;

    [Display(Order =8)]
    [FilterUIHint("Search")]
    public string NeighboorName;

    [Display(Order = 9)]
    [FilterUIHint("Search")]
    public string CategoryName;
 

    [Display(Order = 10)]
    [FilterUIHint("Search")]
    public string BussinesName;
     
     
}

[MetadataType(typeof(RatingTop_DetailMetaData))]
public partial class RatingTop_Detail
{
   
}