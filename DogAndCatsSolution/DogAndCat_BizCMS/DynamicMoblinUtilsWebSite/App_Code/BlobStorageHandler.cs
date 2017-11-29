using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.Hosting;
using System.IO;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
 
public class BlobStorageHandlerManager
{
  

        /// <summary>
        /// WILL SET UP ENVIRONMENT @W_Z_Config. No need to set it up @upper level
        /// </summary>
        /// <returns></returns>
    
   
    public class BlobStorageHandler  
    {
        private static string containerName = UtilsConfig.Get(enumConfigKeys.Storage_Container).Replace("/", "");
        private static string Account { get { return UtilsConfig.Get(enumConfigKeys.Storage_AccountName); } }
        private static string Key { get { return UtilsConfig.Get(enumConfigKeys.Storage_AccountKey); } }

        

        private static StorageCredentialsAccountAndKey StorageCredentialsAccountAndKey
        {
            get
            {
                if (!string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Account))
                    return new StorageCredentialsAccountAndKey(Account, Key);
                else
                    return null;
            }
        }
        private static CloudStorageAccount CloudStorageAccount
        {
            get
            {
                if (StorageCredentialsAccountAndKey != null)
                    return new CloudStorageAccount(StorageCredentialsAccountAndKey,
                                                                                 new Uri(UtilsConfig.Get(enumConfigKeys.Storage_BlobUri)),
                                                                                 new Uri(UtilsConfig.Get(enumConfigKeys.Storage_QueueUri)),
                                                                                 new Uri(UtilsConfig.Get(enumConfigKeys.Storage_TableUri)));
                else
                    return CloudStorageAccount.DevelopmentStorageAccount;
            }
        }
        private static CloudBlobClient CloudBlobClient
        {
            get
            {
                if (StorageCredentialsAccountAndKey != null)
                    return new CloudBlobClient(CloudStorageAccount.BlobEndpoint, StorageCredentialsAccountAndKey);
                else
                    return new CloudBlobClient(CloudStorageAccount.BlobEndpoint, CloudStorageAccount.Credentials);
            }
        }

        public Stream GetFileFromPath(string path)
        {
            string[] cont = path.Split('/');
            string containerName = cont[0];
            string fileName = cont[1];

            CloudBlobContainer objContainer;
            objContainer = CloudBlobClient.GetContainerReference(containerName);

            CloudBlockBlob blob = objContainer.GetBlockBlobReference(fileName);
            return new MemoryStream(blob.DownloadByteArray());
        }

        public void SaveFile(Stream stream, string fullpath, string contenttype, string _containerName)
        {
            if (string.IsNullOrEmpty(_containerName)) _containerName = containerName;

            CloudBlobContainer objContainer;
            objContainer = CloudBlobClient.GetContainerReference(_containerName);
            if (objContainer.CreateIfNotExist())
            {
                objContainer.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Container });
                objContainer.FetchAttributes(new BlobRequestOptions() { Timeout = new TimeSpan(1, 0, 0) });

            }

            CloudBlockBlob objBlob = objContainer.GetBlockBlobReference(fullpath);
            objBlob.Properties.ContentType = contenttype;
            objBlob.UploadFromStream(stream);
        }

        public void DeleteFile(string fullpath, string _containerName)
        {
            if (string.IsNullOrEmpty(_containerName)) _containerName = containerName;

            CloudBlobContainer objContainer;
            objContainer = CloudBlobClient.GetContainerReference(_containerName);

            var blobReference = objContainer.GetBlobReference(fullpath);
            blobReference.DeleteIfExists();
        }

        /// <summary>
        /// List contents of container
        /// </summary>
        /// <param name="containerName">Must pass the exactly container name</param>
        public IList<IListBlobItem> ListContainerContents(string containerName)
        {
            CloudBlobContainer container =
            CloudBlobClient.GetContainerReference(containerName);
            return container.ListBlobs().ToList();
        }
        public List<IListBlobItem> ListPathContents(int depth, string containerName, string accountid, string campaignid)
        {
            CloudBlobClient blobClient = CloudStorageAccount.CreateCloudBlobClient();// storageAccount.CreateCloudBlobClient(); 

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            BlobRequestOptions options = new BlobRequestOptions();
            options.UseFlatBlobListing = true;

            ResultSegment<IListBlobItem> resultSegment = container.ListBlobsSegmented(options);

            IEnumerable<IListBlobItem> mylist = resultSegment.Results;
            IEnumerable<IListBlobItem> mylist2 = (from o in mylist where o.Uri.Segments.Count() == depth && o.Uri.Segments[depth - 3] == accountid.ToString() + "/" && o.Uri.Segments[depth - 2] == campaignid.ToString() + "/" select o);

            return mylist2.ToList();
        }


    }
}