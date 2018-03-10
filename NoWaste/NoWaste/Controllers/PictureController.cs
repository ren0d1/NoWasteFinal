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

        [HttpPost("{id}")]
        public async Task<String> Post(int id, [FromForm]IFormFile file)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                configuration.GetConnectionString("BlobStorage"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(id.ToString());
            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob".
            User user = await unitOfWork.Users.GetById(id);
            int name = user.Adverts.TakeLast(1).FirstOrDefault().Id;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name.ToString());

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return "https://" + httpContextAccessor.HttpContext.Request.Host.ToUriComponent() + "/" + id.ToString()  + "/ " + name.ToString();
        }
    }
}
