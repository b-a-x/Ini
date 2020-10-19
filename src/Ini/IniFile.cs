using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Ini
{
    /// <summary>
    /// Класс для работы с ini
    /// </summary>
    public class IniFile
    {
        private const byte size = 20;

        /// <summary>
        /// Полный путь к файлу
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// kernel32.dll и его функция WritePrivateProfilesString
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        /// <summary>
        /// kernel32.dll и функция GetPrivateProfileString
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string @default, StringBuilder retVal, int size, string filePath);

        public IniFile(string iniPath)
        {
            Path = new FileInfo(iniPath).FullName;
        }

        /// <summary>
        /// Читаем ini-файл и возвращаем значение указного ключа из заданной секции
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadIni(string section, string key)
        {
            var retVal = new StringBuilder(size);
            GetPrivateProfileString(section, key, string.Empty, retVal, size, Path);
            return retVal.ToString();
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        /// <summary>
        /// Удаляем ключ из выбранной секции 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="section"></param>
        public void DeleteKey(string key, string section = null)
        {
            Write(section, key, null);
        }

        /// <summary>
        /// Удаляем выбранную секцию
        /// </summary>
        /// <param name="section"></param>
        public void DeleteSection(string section = null)
        {
            Write(section, null, null);
        }

        /// <summary>
        /// Проверяем, есть ли такой ключ, в этой секции 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool KeyExists(string key, string section = null)
        {
            return ReadIni(section, key).Length > 0;
        }
    }
}
