using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemConsoleApp
{
    internal class Program
    {
        
        /// <summary>
        /// Возвращает список занятых id из файла 
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Список id</returns>
        private static List<String> GetIdList(string path)
        {
            List<String> result = new List<string>();

            if (!File.Exists(path))
            {
                return result;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    result.Add(sr.ReadLine().Split('#')[0]);
                }
            }

            return result;

        }
        
        /// <summary>
        /// Возвращает id для записи
        /// </summary>
        /// <param name="idList">Список занятых id</param>
        /// <returns>id для строки записи</returns>
        private static string AddId(List<String> idList)
        {
            string id;


            while (true)
            {
                Console.Write("ID: ");
                id = Console.ReadLine();

                if (idList.Contains(id)){
                    Console.WriteLine("Этот id уже существует, ведите другой!");
                    continue;
                }
                if (!int.TryParse(id, out int _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }

                break;
            }

            return id;
        }

        /// <summary>
        /// Добавляет текущую дату в строку для записи
        /// </summary>
        /// <returns>Дата для строки записи</returns>
        private static string AddDate()
        {
            Console.WriteLine("Дата добавления записи: " + DateTime.Now.ToShortDateString());
            return DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Добавляет дополнительную информацию в строку для записи
        /// </summary>
        /// <param name="infoLabel">Метка, которая выводится на экран</param>
        /// <param name="isNumeric">Является ли поле числовым</param>
        /// <param name="isDate">Является ли поле датой</param>
        /// <returns>Поле для строки записи</returns>
        private static string AddInfo(string infoLabel, bool isNumeric, bool isDate)
        {
            string info;           
            
            while (true)
            {
                Console.Write(infoLabel);
                info = Console.ReadLine();

                if (isNumeric && !int.TryParse(info, out _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }

                if (isDate && !DateTime.TryParse(info, out _))
                {
                    Console.WriteLine("Введите дату в формате дд.мм.гггг");
                    continue;
                }

                break;
            }
            return info;
        }

        /// <summary>
        /// Генерирует строку для записи
        /// </summary>        
        /// <param name="idList">Список занятых id</param>
        /// <returns>Строка для записи</returns>
        private static string GenerateStringToWrite(List<string> idList)
        {
            string stringToWrite = "";

            Console.WriteLine();
            stringToWrite += AddId(idList) + "#";
            stringToWrite += AddDate() + "#";
            stringToWrite += AddInfo("Ф. И. О.: ", false, false) + "#";
            stringToWrite += AddInfo("Возраст: ", true, false) + "#";
            stringToWrite += AddInfo("Рост: ", true, false) + "#";
            stringToWrite += AddInfo("Дата рождения: ", false, true) + "#";
            stringToWrite += AddInfo("Место рождения: ", false, false);

            return stringToWrite;
        }

        /// <summary>
        /// Генерирует список строк для записи
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Список строк для записи</returns>
        private static List<string> GenerateListToWrite(string path)
        {
            List<string> strings = new List<string>();
            List<String> idList = GetIdList(path);
            
            while (true)
            {
                string nextString = GenerateStringToWrite(idList);
                strings.Add(nextString);
                idList.Add(nextString.Split('#')[0]);
                bool oneMore = false;

                while (true)
                {
                    Console.WriteLine();
                    Console.Write("Хотите сделать ещё одну запись? (y/n) : ");
                    string answer = Console.ReadLine();
                    if (answer != "y" && answer != "n")
                    {
                        continue;
                    }
                    if (answer == "y")
                    {
                        oneMore = true;
                    }
                    break;
                }

                if (!oneMore)
                {
                    break;
                }                
            }

            return strings;
        }

        /// <summary>
        /// Добавление записей в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        private static void EnterData(string path)
        {
            
            List<string> stringsToWrite = GenerateListToWrite(path);

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                foreach (string s in stringsToWrite)
                {                    
                    sw.WriteLine(s);                    
                }               

                Console.WriteLine();
                Console.WriteLine("Добавление записей завершено");
            }
        }

        /// <summary>
        /// Выводит данные из файла в консоль
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        private static void ShowData(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл " + path + " не существует");
                return;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string stringToRead = sr.ReadLine();
                    string[] stringArray = stringToRead.Split('#');

                    Console.WriteLine();
                    Console.WriteLine("ID: " + stringArray[0]);
                    Console.WriteLine("Дата добавления записи: " + stringArray[1]);
                    Console.WriteLine("Ф. И. О.: " + stringArray[2]);
                    Console.WriteLine("Возраст: " + stringArray[3]);
                    Console.WriteLine("Рост: " + stringArray[4]);
                    Console.WriteLine("Дата рождения: " + stringArray[5]);
                    Console.WriteLine("Место рождения: " + stringArray[6]);
                }
            }
        }

        static void Main(string[] args)
        {
            string path = "../../employees.txt";

            while (true)
            {
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1 - вывести данные на экран (клавиша 1)");
                Console.WriteLine("2 - заполнить данные и добавить новую запись в конец файла (клавиша 2)");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    ShowData(path);
                    break;
                                        
                }
                else if (action == "2")
                {
                    EnterData(path);
                    break;
                }
                else
                {
                    Console.Clear();
                }
            }

            Console.ReadKey();
        }
    }
}
