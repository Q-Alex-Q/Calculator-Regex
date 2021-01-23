using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Calculator_2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите выражение или путь к файлу с выражениями для получения результата.");
                string userInput = Console.ReadLine();

                if (File.Exists(userInput))
                {
                    string filePath = userInput;

                    Console.WriteLine("Укажите путь к файлу для хранения результатов выражения.\n" +
                    "Если не указывать путь то результаты будут храниться в файле с именем оригинального файла + Result в той же директории");

                    string destinationPath = Console.ReadLine();
                    if (!File.Exists(destinationPath))
                    {
                        destinationPath = Path.GetPathRoot(filePath) + Path.GetFileNameWithoutExtension(filePath) +
                                                 "Result" + Path.GetExtension(filePath);
                    }
                    Calculator calc = new Calculator();
                    calc.CalculateFileExpressions(filePath, destinationPath);
                }
                else
                {
                    try
                    {
                        Calculator calc = new Calculator();
                        Console.WriteLine($"{userInput} = {calc.GetExpressionResult(userInput)}");
                    }
                    catch (DivideByZeroException e)
                    {
                        Console.WriteLine($"{userInput} = {e.Message}");
                        
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"{userInput} = Выражение имело неверный формат");
                    }
                    
                }
            }

        }
        
    }
}
