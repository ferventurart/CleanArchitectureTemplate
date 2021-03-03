using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFileSystem
    {
        Task<bool> SavePicture(string pictureName, string pictureBase64, CancellationToken cancellationToken);
    }
}
