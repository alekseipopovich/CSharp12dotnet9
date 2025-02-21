Operation multy = (x, y) => x * y;

multy(3, 8);

Operation sum = (x, y) => x + y;

Console.WriteLine(sum(4,5));

int[] integers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

// найдем сумму чисел больше 5
int result1 = Sum(integers, x => x > 5);
Console.WriteLine(result1); // 30

// найдем сумму четных чисел
int result2 = Sum(integers, x => x % 2 == 0);
Console.WriteLine(result2);  //20

int Sum(int[] numbers, IsEqual func)
{
    int result = 0;
    foreach (int i in numbers)
    {
        if (func(i))
            result += i;
    }
    return result;
}


string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };

// создаем новый список для результатов
var selectedPeople = from p in people // передаем каждый элемент из people в переменную p
                     where p.ToUpper().StartsWith("T") //фильтрация по критерию
                     orderby p  // упорядочиваем по возрастанию
                     select p; // выбираем объект в создаваемую коллекцию

foreach (string person in selectedPeople)
    Console.WriteLine(person);


delegate bool IsEqual(int x);

delegate int Operation(int x1, int y1);