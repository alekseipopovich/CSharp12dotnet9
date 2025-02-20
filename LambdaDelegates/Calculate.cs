using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaDelegates
{

    delegate double Operation(double x, double y);
    public class Calculate
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Multiply(double a, double b) => a * b;

        public static void Main(string[] args)
        {
            // Создание экземпляра класса Calculate
            Calculate calculator = new Calculate();

            // Создание экземпляра делегата и связывание с методом Add
            Operation addOperation = new Operation(calculator.Add);

            // Вызов делегата
            double resultAdd = addOperation(5, 3);
            Console.WriteLine($"Результат сложения: {resultAdd}"); // Вывод: 8

            // Создание экземпляра делегата и связывание с методом Multiply
            Operation multiplyOperation = new Operation(calculator.Multiply);

            // Вызов делегата
            double resultMultiply = multiplyOperation(5, 3);
            Console.WriteLine($"Результат умножения: {resultMultiply}"); // Вывод: 15

            Operation multiOperation = addOperation + multiplyOperation;
            double result = multiOperation(5, 3); // Вызовет сначала Add, затем Multiply
        }
    }
}
