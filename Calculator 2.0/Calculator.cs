using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Calculator_2._0
{
    public class Calculator
    {
        /// <summary>
        /// Calculate expressions in file.
        /// </summary>
        /// <param name="sourceFilePath">Path to your file with expressions.</param>
        /// <param name="destinationFilePath">Path where answers will be.</param>
        public void CalculateFileExpressions(string sourceFilePath, string destinationFilePath)
        {
            string[] file = File.ReadAllLines(sourceFilePath);

            for (int i = 0; i < file.Length; i++)
            {
                try
                {
                    file[i] += " = " + GetExpressionResult(file[i]);
                }
                catch (DivideByZeroException e)
                {
                    file[i] += " = " + e.Message;
                }
                catch(Exception)
                {
                    file[i] += " = Выражение имело неверный формат.";
                }
            }

            File.WriteAllLines(destinationFilePath, file);
        }

        public double GetExpressionResult(string expression)
        {
            string[] patternsArray = { @"(?:\d-)?(\-?[\d,]*)(\/|\*)(\-?[\d,]*)",
                                       @"(\-?[\d,]*)(\+|\-)(\-?[\d,]*)" };

            Regex regex = new Regex(@"(\()([^()]+)(\))"); // Паттер выделяет всё что между скобочек(включая и сами скобочки).
            Match match = regex.Match(expression);

            while (match.Success) // Расскрываем скобочки (рекурсивно).
            {
                expression = expression.Replace(match.Value, GetExpressionResult(match.Groups[2].Value).ToString());
                match = regex.Match(expression);
            }

            for (int i = 0; i < patternsArray.Length; i++)
            {
                regex = new Regex(patternsArray[i]);
                match = regex.Match(expression);

                while (match.Groups[1].Value != "") // При match.Success не будет работать корректно.
                {
                    expression = expression.Replace(match.Groups[1].Value + match.Groups[2].Value + match.Groups[3].Value,
                    Calculation(double.Parse(match.Groups[1].Value), match.Groups[2].Value, double.Parse(match.Groups[3].Value)).ToString());

                    match = regex.Match(expression);
                }
            }

            return double.Parse(expression);
        }

        double Calculation(double firstNumber, string action, double secondNumber)
        {
            switch (action)
            {
                case "*":
                    return firstNumber * secondNumber;
                case "/":
                    if (secondNumber == 0)
                    {
                        throw new DivideByZeroException("На ноль делить нельзя.");
                    }
                    return firstNumber / secondNumber;
                case "+":
                    return firstNumber + secondNumber;
                case "-":
                    return firstNumber - secondNumber;
                default: throw new Exception();
            }
        }
    }
}
