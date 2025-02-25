internal class Program1
{
    private static async Task Main1(string[] args)
    {
        await PrintAsync();   // вызов асинхронного метода

        Console.WriteLine("Некоторые действия в методе Main");

        void Print()
        {
            Console.WriteLine("Здесь происходит долгая работа!");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);     // имитация продолжительной работы                
                Console.WriteLine(i);
            }

        }

        // определение асинхронного метода
        async Task PrintAsync()
        {
            //Console.WriteLine("Начало метода PrintAsync"); // выполняется синхронно
            await Task.Run(Print);                // выполняется асинхронно
            Console.WriteLine("Конец метода PrintAsync");
        }
    }
}