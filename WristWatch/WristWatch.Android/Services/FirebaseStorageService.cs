using System.Threading;
using System.Threading.Tasks;
using System.IO;

using WristWatch.Services;
using Firebase.Storage;

namespace WristWatch.Droid.Services
{
    public class FirebaseStorageService: IFirebaseStorageService
    {
        private static readonly string StorageUrl = "wristwatchdb.appspot.com";
        private readonly FirebaseStorage _firebaseStorage = new FirebaseStorage(StorageUrl);

        public async Task<string> LoadImage(Stream fileStream, string fileName, string extension)
        {
            await _firebaseStorage.Child("images").Child($"{fileName}.{extension}")
                .PutAsync(fileStream, CancellationToken.None);
            return await _firebaseStorage.Child("images").Child($"{fileName}.{extension}").GetDownloadUrlAsync();
        }

        public async Task<string> LoadVideo(Stream fileStream, string fileName, string extension)
        {
            await _firebaseStorage.Child("videos").Child($"{fileName}.{extension}")
                .PutAsync(fileStream, CancellationToken.None, "video/mp4");

            return await _firebaseStorage.Child("videos").Child($"{fileName}.{extension}").GetDownloadUrlAsync();
        }

        public async Task RemoveImage(string fileName)
        {
            await _firebaseStorage
                .Child("images")
                .Child($"{fileName}.jpg")
                .DeleteAsync();
        }

        public async Task RemoveVideo(string fileName)
        {
            await _firebaseStorage
                .Child("videos")
                .Child($"{fileName}.mp4")
                .DeleteAsync();
        }
    }
}