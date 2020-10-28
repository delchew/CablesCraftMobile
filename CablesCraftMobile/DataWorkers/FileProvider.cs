using System;
using System.IO;
using System.Reflection;

namespace CablesCraftMobile
{
    public static class FileProvider
    {
        /// <summary>
        /// Копирует файл с потоком в папку установки приложения на устройство, при условии, что папка не содержит файл с таким именем.
        /// Метод возвращает путь по которому будет лежать или лежит уже существующий файл.
        /// </summary>
        /// <param name="resourceFileName">Имя файла в проекте</param>
        /// <returns></returns>
        public static string SendResourceFileToLocalApplicationFolder(string resourceFileName)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), resourceFileName);
            if (!File.Exists(filePath))
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                using (var stream = assembly.GetManifestResourceStream($"{typeof(App).Namespace}.JsonDataFiles.{resourceFileName}"))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
            }
            return filePath;
        }
    }
}
