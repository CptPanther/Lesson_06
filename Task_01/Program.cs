using System;
using static System.Console;
using System.IO;
using System.Text;

internal class Program
{
    //Основная программа
    static void Main()
    {
        string fileName = "personal.txt";
        string action = string.Empty;
        string[] position = {
                        "\nID сотрудника:",
                        "Дата и время внесения в список: ",
                        "Ф.И.О.: ",
                        "Возраст: ",
                        "Рост: ",
                        "Дата рождения: ",
                        "Место рождения: " };

        while (true) 
        {
            Clear();
            WriteLine("\n1. Вывод данных на экран.");
            WriteLine("2. Заполнить данные сотрудника.");
            WriteLine("0. Выход");
            Write("\nВыберите действие: ");
            action = ReadLine();

            if (action == "0") 
                break;
            else if (action == "1")
                DrawDataList(fileName, position);
            else if (action == "2")
                SetData(fileName, position);
            else
                WriteLine("\nУказан неверный параметр.");
        }
    }

    //Считывание данных с файла и вывод списка сотрудников.
    static void DrawDataList(string fName, string[] position)
    {
        FileInfo file = new FileInfo(fName);
        if (file.Exists)
        {
            string line;

            Clear();

            WriteLine("\nСписок персонала: ");

            using (StreamReader sr = new StreamReader(fName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] dataPerson = line.Split('#');

                    for (int step = 0; step < dataPerson.Length; step++)
                        WriteLine($"{position[step]}{dataPerson[step]}");
                }
            }
        }
        else
            WriteLine("\nФайл отсутствует. Необходимо составить список сотрудников.");
        ReadKey(true);
    }

    //Запись данных о сотрудниках в файл.
    static void SetData(string fName, string[] position) 
    {
        int idCounter = 0; 
        char key = 'д';

        if (File.Exists(fName))
            idCounter = File.ReadAllLines(fName).Length;

        Clear();

        do
        {
            using (StreamWriter sw = new StreamWriter(fName, true))
            {
                idCounter++;
                StringBuilder line = new StringBuilder(idCounter.ToString());
                WriteLine($"ID сотрудника: {line}");
                
                string dateTime = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                WriteLine($"Дата и время добавления записи: {dateTime}");
                line.AppendFormat($"#{dateTime}");
                
                for (int step = 2; step < position.Length; step++)
                {
                    Write(position[step]);
                    line.AppendFormat($"#{ReadLine()}");
                }

                sw.WriteLine(line);

                WriteLine("Продолжить запись?(д/н)");
                key = ReadKey(true).KeyChar;
            }
        } while (char.ToLower(key) == 'д');
    }
}
