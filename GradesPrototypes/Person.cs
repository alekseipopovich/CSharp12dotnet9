using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Person
{
    // Поля структуры
    public string FirstName { get; set; } // Имя
    public string LastName { get; set; }  // Фамилия
    public int Age { get; set; }          // Возраст
    public string Address { get; set; }   // Адрес

    // Конструктор для инициализации полей
    public Person(string firstName, string lastName, int age, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Address = address;
    }

    // Метод для вывода информации о человеке
    public void PrintInfo()
    {
        Console.WriteLine($"Имя: {FirstName} {LastName}");
        Console.WriteLine($"Возраст: {Age}");
        Console.WriteLine($"Адрес: {Address}");
    }

    public override string ToString()
    {
        return $"Имя: {FirstName} {LastName}, Возраст: {Age}, Адрес: {Address}";
    }

    // Метод для проверки, является ли человек совершеннолетним
    public bool IsAdult()
    {
        return Age >= 18;
    }


}
