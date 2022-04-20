using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristWatch.Services;
using WristWatch.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace WristWatch.Droid.Services
{
    public class FirebaseDatabaseService : IFirebaseDatabaseService
    {
        private static readonly string BaseUrl = "https://wristwatchdb-default-rtdb.europe-west1.firebasedatabase.app/";
        private static readonly string NoPictureUrl = "https://firebasestorage.googleapis.com/v0/b/wristwatchdb.appspot.com/o/images%2Fdf672b2a-5640-479e-8c9b-7b531a4f6af2.jpg?alt=media&token=5410b878-323c-4a59-97f9-3c01336df760";

        private readonly FirebaseClient _databaseClient = new FirebaseClient(BaseUrl);

        public async Task AddWatch(Watch Data)
        {
            await _databaseClient.Child("Watches").PostAsync(Data);
        }

        public List<Watch> GetAllWatches()
        {
            try
            {
                var taskGetAllWatches = _databaseClient.Child("Watches").OnceAsync<Watch>();

                taskGetAllWatches.Wait();
                if (taskGetAllWatches.Exception != null)
                {
                    Console.WriteLine(taskGetAllWatches.Exception.Message);
                    return null;
                }

                IEnumerable<FirebaseObject<Watch>> resultList = taskGetAllWatches.Result;
                return resultList.Select(item => new Watch
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Description = item.Object.Description,
                    Type = item.Object.Type,
                    Width = item.Object.Width,
                    Thickness = item.Object.Thickness,
                    Price = item.Object.Price,
                    Image = new CloudFileData
                    {
                        FileName = item.Object.Image?.FileName ?? "",
                        DownloadUrl = item.Object.Image?.DownloadUrl ?? NoPictureUrl
                    },
                    Video = new CloudFileData
                    {
                        FileName = item.Object.Video?.FileName ?? "",
                        DownloadUrl = item.Object.Video?.DownloadUrl ?? ""
                    }
                }).ToList();
            }
            catch (Exception ex)
            {
            }
            return new List<Watch>();
        }

        public Watch GetWatchById(string id)
        {
            var taskGetAllWatches = _databaseClient
                .Child("Watches")
                .OnceAsync<Watch>();

            taskGetAllWatches.Wait();

            if (taskGetAllWatches.Exception != null)
            {
                Console.WriteLine(taskGetAllWatches.Exception.Message);
                return null;
            }

            IEnumerable<FirebaseObject<Watch>> resultWatches = taskGetAllWatches.Result;

            return resultWatches.Where(c => c.Object.Id == id).Select(item => new Watch
            {
                Id = item.Object.Id,
                Name = item.Object.Name,
                Description = item.Object.Description,
                Type = item.Object.Type,
                Width = item.Object.Width,
                Thickness = item.Object.Thickness,
                Price = item.Object.Price,
                Image = new CloudFileData
                {
                    FileName = item.Object.Image?.FileName ?? "",
                    DownloadUrl = item.Object.Image?.DownloadUrl ?? "https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty.jpg"
                },
                Video = new CloudFileData
                {
                    FileName = item.Object.Video?.FileName ?? "",
                    DownloadUrl = item.Object.Video?.DownloadUrl ?? ""
                }
            }).FirstOrDefault();
        }

        public async Task UpdateWatch(string id, Watch Data)
        {
            var toUpdateWatches = (await _databaseClient
                .Child("Watches")
                .OnceAsync<Watch>()).FirstOrDefault(c => c.Object.Id == id);

            await _databaseClient
                .Child("Watches")
                .Child(toUpdateWatches?.Key)
                .PutAsync(Data);
        }
    }
}