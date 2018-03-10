using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NoWaste.Models;
using NoWaste.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoWaste.Controllers
{
    [Produces("application/json")]
    [Route("api/Picture")]
    public class PictureController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly UnitOfWork unitOfWork;

        public PictureController(IConfiguration configuration, UnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }

        public async Task<String> Post([FromForm]IFormFile picture)
        {
            return await PictureUploader(User.Identity.Name ?? Guid.NewGuid().ToString(), picture);
        }

        private async Task<string> PictureUploader(string containerName, IFormFile picture){
            string userName = (User.Identity.Name + DateTime.Now) ?? Guid.NewGuid().ToString();

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                configuration.GetConnectionString("BlobStorage"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, null, null);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(userName);

            await blockBlob.UploadFromStreamAsync(picture.OpenReadStream());

            return "https://nowaste.blob.core.windows.net" + "/" + containerName + "/" + userName;
        }
    }
}
