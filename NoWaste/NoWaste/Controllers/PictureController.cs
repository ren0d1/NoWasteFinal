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
using System.Threading.Tasks;

namespace NoWaste.Controllers
{
    [Produces("application/json")]
    [Route("api/Picture")]
    public class PictureController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PictureController(IConfiguration configuration, UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        //[HttpPost("{id}")]
        public async Task<String> Post([FromForm]IFormFile picture)
        {
            var userName = User.Identity.Name ?? Guid.NewGuid().ToString();
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                configuration.GetConnectionString("BlobStorage"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(userName);
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container,null,null);

            // Retrieve reference to a blob named "myblob".
            //User user = unitOfWork.Users.GetUserByName(userName);
            //string photoName = user.Adverts.TakeLast(1).FirstOrDefault().Id.ToString();
            string photoName = Guid.NewGuid().ToString();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(photoName);

            await blockBlob.UploadFromStreamAsync(picture.OpenReadStream());
            var test = "https://nowaste.blob.core.windows.net" + "/" + userName + "/" + photoName;
            return test;
            return "https://" + httpContextAccessor.HttpContext.Request.Host.ToUriComponent() + "/" + userName + "/ " + photoName;
        }
    }
}
