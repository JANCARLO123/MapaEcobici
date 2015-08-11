using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Ecobici.WP.Common
{
    public class JsonController<T> where T : class
    {
        private readonly string _fileName;
        private T _element;

        public JsonController(string fileName)
        {
            this._fileName = fileName;
        }

        public async Task SaveElementAsync(T element)
        {
            _element = element;
            IStorageFolder applicationFolder =
                ApplicationData.Current.LocalFolder;

            IStorageFile storageFile = await applicationFolder.CreateFileAsync(_fileName, CreationCollisionOption.OpenIfExists);

            var jsonResult = JsonConvert.SerializeObject(element);

            await Windows.Storage.FileIO.WriteTextAsync(storageFile, jsonResult);
        }

        public async Task<T> ReadElementAsync()
        {
            T result = null;

            try
            {
                IStorageFolder storageFolder = ApplicationData.Current.LocalFolder;

                IStorageFile storageFile = await storageFolder.CreateFileAsync(_fileName, CreationCollisionOption.OpenIfExists);

                string fileTextResult = await Windows.Storage.FileIO.ReadTextAsync(storageFile);

                result = JsonConvert.DeserializeObject<T>(fileTextResult);
            }
            catch (Exception)
            {
            }

            return result;
        }
    }
}