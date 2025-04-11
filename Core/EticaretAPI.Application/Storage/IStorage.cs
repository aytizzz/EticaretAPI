using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathorContainerName)>> UploadAsync(string pathorContainerName, 
            IFormFileCollection files);
        Task DeleteAsync(string pathorContainerName, string fileName);
        List<string> GetFiles(string pathorContainerName);
        bool HasFile(string pathorContainerName, string fileName);

    }
}
