using System.Threading.Tasks;
using System.IO;
namespace WristWatch.Services
{
    public interface IFirebaseStorageService
    {
        Task<string> LoadImage(Stream fileStream, string fileName, string extension);

        Task<string> LoadVideo(Stream fileStream, string fileName, string extension);

        Task RemoveImage(string fileName);

        Task RemoveVideo(string fileName);
    }
}
