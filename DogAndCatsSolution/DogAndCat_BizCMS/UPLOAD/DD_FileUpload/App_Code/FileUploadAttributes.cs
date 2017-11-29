using System;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class FileUploadAttribute : Attribute
{
	/// <summary>
	/// where to save files
	/// </summary>
	public String FileUrl { get; set; }

	/// <summary>
	/// File tyoe to allow upload
	/// </summary>
	public String[] FileTypes { get; set; }

	/// <summary>
	/// image type to use for displaying file icon
	/// </summary>
	public String DisplayImageType { get; set; }

	/// <summary>
	/// where to find file type icons
	/// </summary>
	public String DisplayImageUrl { get; set; }

	/// <summary>
	/// If present user must be a member of one 
	/// of the roles to be able to download file
	/// </summary>
	public String[] HyperlinkRoles { get; set; }

	/// <summary>
	/// Used to Disable Hyperlink (Enabled by default)
	/// </summary>
	public Boolean DisableHyperlink { get; set; }

	/// <summary>
	/// helper method to check for roles in this attribute
	/// the comparison is case insensitive
	/// </summary>
	/// <param name="role"></param>
	/// <returns></returns>
	public bool HasRole(String[] roles)
	{
		if (HyperlinkRoles.Count() > 0)
		{
			var hasRole = from hr in HyperlinkRoles.AsEnumerable()
						  join r in roles.AsEnumerable()
						  on hr.ToLower() equals r.ToLower()
						  select true;

			return hasRole.Count() > 0;
		}
		return false;
	}

}

public static class FileUploadHelper
{
	/// <summary>
	/// If the given table contains a column that has a UI Hint with the value "DbImage", finds the ScriptManager
	/// for the current page and disables partial rendering
	/// </summary>
	/// <param name="page"></param>
	/// <param name="table"></param>
	public static void DisablePartialRenderingForUpload(Page page, MetaTable table)
	{
		foreach (var column in table.Columns)
		{
			// TODO this depends on the name of the field template, need to fix
			if (String.Equals(
				column.UIHint, "DBImage", StringComparison.OrdinalIgnoreCase) ||
				String.Equals(column.UIHint, "FileImage", StringComparison.OrdinalIgnoreCase) ||
				String.Equals(column.UIHint, "FileUpload", StringComparison.OrdinalIgnoreCase))
			{
				var sm = ScriptManager.GetCurrent(page);
				if (sm != null)
				{
					sm.EnablePartialRendering = false;
				}
				break;
			}
		}
	}
}