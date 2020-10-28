using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CablesCraftMobile
{
    public class JsonRepository
    {
        public IList<T> GetObjects<T>(FileInfo dataFileInfo, string jsonDataPath)
        {
            var selectedJsonData = GetCurrentJsonDataToDeserialize(dataFileInfo, jsonDataPath);
            return JsonConvert.DeserializeObject<List<T>>(selectedJsonData);
        }

        public IList<T> GetObjects<T>(FileInfo dataFileInfo)
        {
            return GetObjects<T>(dataFileInfo, string.Empty);
        }

        public IDictionary<TKey, TValue> GetObjects<TKey, TValue>(FileInfo dataFileInfo, string jsonDataPath)
        {
            var selectedJsonData = GetCurrentJsonDataToDeserialize(dataFileInfo, jsonDataPath);
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(selectedJsonData);
        }

        public IDictionary<TKey, TValue> GetObjects<TKey, TValue>(FileInfo dataFileInfo)
        {
            return GetObjects<TKey, TValue>(dataFileInfo, string.Empty);
        }

        private string GetCurrentJsonDataToDeserialize(FileInfo dataFileInfo, string jsonDataPath)
        {
            if (dataFileInfo.Exists)
            {
                var loadedData = File.ReadAllText(dataFileInfo.FullName, Encoding.UTF8);
                if (!string.IsNullOrEmpty(loadedData))
                {
                    if (!string.IsNullOrEmpty(jsonDataPath))
                    {
                        var jObject = JObject.Parse(loadedData);
                        var jsonSelectedData = jObject.SelectToken(jsonDataPath);
                        return jsonSelectedData.ToString();
                    }
                    return loadedData;
                }
                throw new FileLoadException($"Отсутствуют данные в файле {dataFileInfo.Name}!");
            }
            throw new FileNotFoundException($"Файл {dataFileInfo.Name} не найден! Проверьте правильность пути к файлу.");
        }
    }
}
