Message mes;            // 2. Создаем переменную делегата
mes = Hello;            // 3. Присваиваем этой переменной адрес метода
mes();                  // 4. Вызываем метод

Message mesCalculate = Calculate;
mesCalculate();
void Hello() => Console.WriteLine("Hello world!");
void Calculate() => Console.WriteLine("2+2 = 4");

delegate void Message(); // 1. Объявляем делегат