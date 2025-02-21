using System.Text.Json;

Person tom = new Person("Tom", 37);
string json = JsonSerializer.Serialize(tom);
Console.WriteLine(json);
Person? restoredPerson = JsonSerializer.Deserialize<Person>(json);
Console.WriteLine(restoredPerson?.Name); // Tom
 
// сохранение данных
using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
{
    await JsonSerializer.SerializeAsync<Person>(fs, tom);
    Console.WriteLine("Data has been saved to file");
}

Person? person;

// чтение данных
using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
{
    person = await JsonSerializer.DeserializeAsync<Person>(fs);
    Console.WriteLine($"Name: {person?.Name}  Age: {person?.Age}");
}

class Person
{
    public string Name { get; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}