Message mes = SomeMethods.Hello;
//mes();

//Greet greet = SomeMethods.GreetingName;

//Console.WriteLine(greet("Мир"));

Message message1 = MessageStudy;

Message message2 = delegate ()
{
    Console.WriteLine("Все будет хорошо!");
};

Message common = mes + message1 + message2;

common.Invoke();
void MessageStudy() => Console.WriteLine("Изучаем C#");




