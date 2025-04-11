using EticaretAPI.Application.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName => throw new NotImplementedException();

        public async Task DeleteAsync(string pathorContainerName, string fileName)
        => await _storage.DeleteAsync(pathorContainerName, fileName);



        public List<string> GetFiles(string pathorContainerName)
        => _storage.GetFiles(pathorContainerName);



        public bool HasFile(string pathorContainerName, string fileName)
       => _storage.HasFile(pathorContainerName, fileName);

        public Task<List<(string fileName, string pathorContainerName)>> UploadAsync(string pathorContainerName, IFormFileCollection files)
        => _storage.UploadAsync(pathorContainerName, files);
    }
}
