using System;
using System.IO;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.UI;
using Microsoft.Web.DynamicData;

public partial class FileImage : FieldTemplateUserControl
{

	public override Control DataControl
	{
		get
		{
			return Label1;
		}
	}

	protected override void OnDataBinding(EventArgs e)
	{
		base.OnDataBinding(e);

		//check if field has a value
		if (FieldValue == null)
			return;

		// get the file extension
		String extension = FieldValueString.Substring(
			FieldValueString.LastIndexOf(".") + 1,
			FieldValueString.Length - (FieldValueString.LastIndexOf(".") + 1));

		// get attributes
		var fileUploadAttributes = MetadataAttributes.OfType<FileUploadAttribute>().FirstOrDefault();
		String fileUrl = fileUploadAttributes.FileUrl;
		String displayImageUrl = fileUploadAttributes.DisplayImageUrl;
		String displayImageType = fileUploadAttributes.DisplayImageType;

		// check the file exists else throw validation error
		String filePath;
		if (fileUploadAttributes != null)
			filePath = String.Format(fileUrl, FieldValueString);
		else
			// if attribute not set use default
			filePath = String.Format("~/files/{0}", FieldValueString);

		// show the relavent control depending on metadata
		if (fileUploadAttributes.HyperlinkRoles != null)
		{
			// if there are roles then check:  
			// if user is in one of the roles supplied
			// or if the hyperlinks are disabled  
			// or if the file does not exist 
			// then hide the link
			if (!fileUploadAttributes.HasRole(Roles.GetRolesForUser()) || fileUploadAttributes.DisableHyperlink || !File.Exists(Server.MapPath(filePath)))
			{
				Label1.Text = FieldValueString;
				HyperLink1.Visible = false;
			}
			else
			{
				Label1.Visible = false;
				HyperLink1.Text = FieldValueString;
				HyperLink1.NavigateUrl = filePath;
			}
		}
		else
		{
			// if either hyperlinks are disabled or the 
			// file does not exist then hide the link
			if (fileUploadAttributes.DisableHyperlink || !File.Exists(Server.MapPath(filePath)))
			{
				Label1.Text = FieldValueString;
				HyperLink1.Visible = false;
			}
			else
			{
				Label1.Visible = false;
				HyperLink1.Text = FieldValueString;
				HyperLink1.NavigateUrl = filePath;
			}
		}

		// check file exists on file system
		if (!File.Exists(Server.MapPath(filePath)))
		{
			CustomValidator1.ErrorMessage = String.Format("{0} does not exist", FieldValueString);
			CustomValidator1.IsValid = false;
		}

		// show the icon
		if (!String.IsNullOrEmpty(extension))
		{
			// set the file type image
			if (!String.IsNullOrEmpty(displayImageUrl))
			{
				Image1.ImageUrl = String.Format(displayImageUrl, extension + "." + displayImageType);
			}
			else
			{
				// if attribute not set the use default
				Image1.ImageUrl = String.Format("~/images/{0}", extension + "." + displayImageType);
			}

			Image1.AlternateText = extension + " file";

			// if you apply dimentions from DD Futures
			var imageFormat = MetadataAttributes.OfType<ImageFormatAttribute>().FirstOrDefault();
			if (imageFormat != null)
			{
				// if either of the dims is 0 don't set it
				// this will mean that the aspect will remain locked
				if (imageFormat.DisplayWidth != 0)
					Image1.Width = imageFormat.DisplayWidth;
				if (imageFormat.DisplayHeight != 0)
					Image1.Height = imageFormat.DisplayHeight;
			}
		}
		else
		{
			// if file has no extension then hide image
			Image1.Visible = false;
		}
	}
}