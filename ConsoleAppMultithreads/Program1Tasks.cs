using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMultithreads
{
    class Program1Tasks
    {
        static void Main1(string[] args)
        {
            Console.WriteLine("Main thread started.");

            Task task1 = new Task(() => Console.WriteLine("Hello Task START"));
            task1.Start();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Hello Task FACTORY STARTNEW"));

            // Создание и запуск задачи с использованием лямбда-выражения
            Task task = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Task Run iteration {i}");
                    Task.Delay(500).Wait(); // Имитация работы
                }
            });

            // Ожидание завершения задачи
            task.Wait();

            Console.WriteLine("Main thread finished.");
        }
    }
}
