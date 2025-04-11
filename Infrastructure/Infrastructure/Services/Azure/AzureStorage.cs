using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EticaretAPI.Application.Storage;
using EticaretAPI.Infrastructure.Services.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Azure
{
    public class AzureStorage : Storagee,  IStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _BlobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string ContainerName, string fileName)
        {
            _BlobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = _BlobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string ContainerName)
        {
            _BlobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            return _BlobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFile(string ContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string fileName, string pathorContainerName)>> UploadAsync(string ContainerName, IFormFileCollection files)
        {
            _BlobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            await _BlobContainerClient.CreateIfNotExistsAsync();
            await _BlobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathorContainerName)> datas = new ();

            foreach (IFormFile file in files)
            {

                FileRenameAsync(ContainerName, file.Name, HasFile);
                 BlobClient blobClient=  _BlobContainerClient.GetBlobClient(file.Name);
               await blobClient.UploadAsync(file.OpenReadStream());
                //datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
            }
            return datas;
        }
    }
}
