using System;
using System.IO;

namespace FileSystemConsoleApp
{
    internal class Program
    {
        
        /// <summary>
        /// Выводит данные из файла в консоль
        /// </summary>
        private static void ShowData()
        {
            using (StreamReader sr = new StreamReader("../../employees.txt"))
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

        /// <summary>
        /// Добавляет записи в файл
        /// </summary>
        private static void EnterData()
        {
            
            using (StreamWriter sw = new StreamWriter("../../employees.txt", true))
            {
                while (true)
                {
                    string stringToWrite = "";
                    bool oneMore = false;

                    Console.WriteLine();
                    Console.Write("ID: ");
                    stringToWrite += Console.ReadLine() + "#";
                    Console.WriteLine("Дата добавления записи: " + DateTime.Now.ToShortDateString());
                    stringToWrite += DateTime.Now.ToShortDateString() + "#";
                    Console.Write("Ф. И. О.: ");
                    stringToWrite += Console.ReadLine() + "#";
                    Console.Write("Возраст: ");
                    stringToWrite += Console.ReadLine() + "#";
                    Console.Write("Рост: ");
                    stringToWrite += Console.ReadLine() + "#";
                    Console.Write("Дата рождения: ");
                    stringToWrite += Console.ReadLine() + "#";
                    Console.Write("Место рождения: ");
                    stringToWrite += Console.ReadLine();

                    sw.WriteLine(stringToWrite);

                    while (true) {
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

                Console.WriteLine();
                Console.WriteLine("Добавление записей завершено");
            }
        }
        
        static void Main(string[] args)
        {
            if (!File.Exists(@"../../employees.txt"))
            {                
                File.Create(@"../../employees.txt");
            }

            while (true)
            {
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1 - вывести данные на экран (клавиша 1)");
                Console.WriteLine("2 - заполнить данные и добавить новую запись в конец файла (клавиша 2)");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    ShowData();
                    break;
                                        
                }
                else if (action == "2")
                {
                    EnterData();
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
