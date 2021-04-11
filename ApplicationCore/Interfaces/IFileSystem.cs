using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFileSystem
    {
        Task<string> SaveImage(Stream stream, string fileName, string bucketName);
    }
}
