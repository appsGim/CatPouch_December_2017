using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Web.DynamicData;

[MetadataType(typeof(FileImageTestMD))]
public partial class FileImageTest : INotifyPropertyChanging, INotifyPropertyChanged
{
	public class FileImageTestMD
	{
		public object Id { get; set; }
		public object Description { get; set; }
		[UIHint("FileUpload")]
		[FileUpload(
			FileUrl = "~/files/{0}",
			FileTypes = new String[] { "pdf", "xls", "doc", "xps" },
			DisplayImageType = "png",
			DisableHyperlink = false,
			HyperlinkRoles=new String[] { "Admin", "Accounts" },
			DisplayImageUrl = "~/images/{0}")]
		[ImageFormat(22, 0)]
		public object filePath { get; set; }
	}
}