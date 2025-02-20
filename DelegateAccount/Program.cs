// создаем банковский счет
Account account = new Account(200);
// Добавляем в делегат ссылку на метод PrintSimpleMessage
account.RegisterHandler(PrintSimpleMessage);
// Два раза подряд пытаемся снять деньги
account.Take(100);
account.Take(150);

void PrintSimpleMessage(string message) => Console.WriteLine(message);
