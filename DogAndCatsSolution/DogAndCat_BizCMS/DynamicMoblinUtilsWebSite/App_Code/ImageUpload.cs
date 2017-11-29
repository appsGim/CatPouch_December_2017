using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System;

public class ImageUploadMetaData
{
    [FilterUIHint("Int64_Range")]
    public long ID;

    [FilterUIHint("Date")]
    public string InsertDate;

}

[MetadataType(typeof(ImageUploadMetaData))]
public partial class ImageUpload
{
   
}