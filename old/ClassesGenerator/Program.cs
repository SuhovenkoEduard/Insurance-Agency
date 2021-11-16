using ClassesGenerator.Database;
using ClassesGenerator.Generator;
using ClassesGenerator.Writer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ClassesGenerator
{
    class Program
    {
        static string DirPath = "D:\\Projects\\Sem_4\\OOP Kurs\\CourseApp\\Classes\\";

        static void Main(string[] args)
        {
            // load data
            List<TableInfo> tables = DataLoader.Load();
            Console.WriteLine("Загрузка данных завершена.");


            // generate code
            List<KeyValuePair<string, string>> files = new List<KeyValuePair<string, string>>();
            foreach (var table in tables)
                files.Add(new KeyValuePair<string, string> (DirPath + table.Name + ".cs", ClassGenerator.Generate(table)));
            Console.WriteLine("Генерация кода завершена.");


            // write in files
            foreach (var pair in files)
                MyWriter.Write(pair.Key, pair.Value);
            Console.WriteLine("Запись в файлы завершена.");

            Console.ReadKey();
        }
    }
}
