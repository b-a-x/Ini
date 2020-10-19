using System;
using System.IO;

namespace Ini
{
    public class Program
    {
        private const byte s = 1;
        private const byte k = 3;
        
        static void Main(string[] args)
        {
            IniFile ini = new IniFile("config.ini");

            ini.Write("Test", "Test", "Test");

            if (File.Exists(ini.Path) == false)
            {
                Console.WriteLine("Отсутствует файл config.ini");
                return;
            }

            try
            {
                if (ini.KeyExists(args[k], args[s]) == false)
                {
                    Console.WriteLine("Заданной пары СЕКЦИЯ ПАРАМЕТР нет в конфигурационном файл");
                    return;
                }

                Console.WriteLine($"Секция: {args[s]}");
                Console.WriteLine($"Ключ: {args[k]}");
                Console.WriteLine($"Значение: {ini.ReadIni(args[s], args[k])}");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Неправлино переданы параметры");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка приложения");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
