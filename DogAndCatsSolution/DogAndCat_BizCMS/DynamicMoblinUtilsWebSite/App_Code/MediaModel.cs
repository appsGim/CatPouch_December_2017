using System; 
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class UtilsImage
{
     
    public static Bitmap CreateVarification(string data)
    {
        Bitmap flag = new Bitmap(100,35);
        Graphics flagGraphics = Graphics.FromImage(flag);
        Font drawFont = new Font("Courier", 16);
        SolidBrush drawBrush = new SolidBrush(Color.WhiteSmoke);

        flagGraphics.FillRectangle(Brushes.Red, 0, 0, 100, 35);
        flagGraphics.DrawString(data,drawFont, drawBrush,12,7);


        return flag;
    } 

    public static string ImageToBase64(Image image, ImageFormat format)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Convert Image to byte[]
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }

    public static Image Base64ToImage(string base64String)
    {
        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0,
                                                                             imageBytes.Length);

        // Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length);
        Image image = Image.FromStream(ms, true);
        return image;
    }

    public static Stream ToStream(Image image, ImageFormat formaw)
    {
        var stream = new System.IO.MemoryStream();
        image.Save(stream, formaw);
        stream.Position = 0;
        return stream;
    }

    public static ImageFormat getImageFormatFromExtension(string extension)
    {
        switch (extension)
        {
            case ".gif": return ImageFormat.Gif;
            case ".jpg": return ImageFormat.Jpeg;
            case ".jpeg": return ImageFormat.Jpeg;
            case ".png": return ImageFormat.Png;
        }
        return null;
    }

    public static string get_ContentType(string extension)
    {
        switch (extension)
        {
            case ".gif": return "image/gif";
            case ".jpg": return "image/jpeg";
            case ".jpeg": return "image/jpeg";
            case ".png": return "image/png";
        }
        return null;
    }

    public static object Base64_WithResize(enumReturnResizeObject ReturnType, string base64image, int width, int height, out Exception exc)
    {
        exc = null;

        try
        {
            Image img = UtilsImage.Base64ToImage(base64image);

            Bitmap newImage = new Bitmap(img, width, height);//Bitmap(newWidth, newHeight);

            MemoryStream memoryStream = new MemoryStream();
            /*bitmap*/
            newImage.Save(memoryStream, /*ImageFormat.Png*/img.RawFormat);
            //var pngData = memoryStream.ToArray();	
            //byte[] imageBytes =pngData;// Convert.FromBase64String(base64String);

            byte[] imageBytes = memoryStream.ToArray();

            MemoryStream ms = new MemoryStream(imageBytes, 0,
                                                                                 imageBytes.Length);
            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);

            if (ReturnType == enumReturnResizeObject.MemoryStream) return ms;

            Image image = Image.FromStream(ms, true);

            if (ReturnType == enumReturnResizeObject.IMAGE) return image;

            return ImageToBase64(image, image.RawFormat);

        }
        catch (Exception ex)
        {
            exc = ex;
        }
        return null;
    }

    public enum enumReturnResizeObject { Base64String, IMAGE, MemoryStream }
}
/// <summary>
/// Summary description for MediaModel
/// </summary>
 
    public enum MediaTypeValueID
    {
        Image = 1
    }

    public class MediaModel : BlobStorageHandlerManager 
    {
        public string name { get; set; }

        public int? mediatype { get; set; }

        public int? destinationplatformtype { get; set; }

        public string location { get; set; }

        public MediaModel()
        {
        }

        public MediaModel(int? mediaid)
        {
        }

        private string getMediaData(ImageFormat format)
        {
            string ext = "";
            if (format.Equals(ImageFormat.Jpeg))
            {

                mediatype = (int)MediaTypeValueID.Image;
                ext = ".jpeg";
            }
            else
                if (format.Equals(ImageFormat.Gif))
            {
                ext = ".gif";
                mediatype = (int)MediaTypeValueID.Image;
            }
            else
                    if (format.Equals(ImageFormat.Png))
            {
                ext = ".png";
                mediatype = (int)MediaTypeValueID.Image;
            }
            return ext;
        }

        private ImageFormat getImageFormatFromExtension(string extension)
        {
            return UtilsImage.getImageFormatFromExtension(extension);
        }
        private string getContentType(string extension)
        {
            return UtilsImage.get_ContentType(extension);
        }

        public bool DeleteImage(string location)
        {
        BlobStorageHandlerManager.BlobStorageHandler sh = new BlobStorageHandlerManager.BlobStorageHandler();
            location = location.Replace(UtilsConfig.Get(enumConfigKeys.Storage_BlobUri) + UtilsConfig.Get(enumConfigKeys.Storage_Container), "");
            sh.DeleteFile(location, UtilsConfig.Get(enumConfigKeys.Storage_Container));
            return true;
        }

    /// <summary>
    /// 0 - NEW IMAGE NAME
    /// 1 - LOCATION WITH NAME
    /// </summary>
    /// <param name="newname"></param>
    /// <param name="containerName"></param>
    /// <param name="i"></param>
    /// <returns></returns>
        public string[] Save( string newname, string containerName, Image i)
        {
            string cleanImage = null;

        try
            {
                BlobStorageHandlerManager.BlobStorageHandler sh = new BlobStorageHandlerManager.BlobStorageHandler();
                Image thisImage = i;
                string extension = getMediaData(thisImage.RawFormat);
                string newImageName = string.Empty;
                ImageFormat imageFormat = getImageFormatFromExtension(extension);
                string contenttype = getContentType(extension);

                cleanImage = newname + extension;
                newImageName = cleanImage;

                if (imageFormat != null)
                {
                    sh.SaveFile(UtilsImage.ToStream(thisImage, imageFormat), newImageName, contenttype, containerName);
                    location = newImageName;
                    location = UtilsConfig.Get(enumConfigKeys.Storage_BlobUri) + containerName + location;
                 
                    return new string[] { newImageName, location };
                }
            }
            catch (Exception ex)
            {
             
            }

            return null;
        }
    }

 