using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Структура для студента
public struct Student
{

    public int Age { get; set; }
    public string Name { get; set; } // Имя студента
    public List<Grade> Grades { get; set; } // Список оценок

    // Конструктор для инициализации
    public Student(string name)
    {
        Name = name;
        Grades = new List<Grade>();
    }

    // Метод для добавления оценки
    public void AddGrade(Subject subject, int score, DateTime date)
    {
        Grades.Add(new Grade(subject, score, date));
    }

    // Метод для удаления оценки по индексу
    public void RemoveGrade(int index)
    {
        if (index >= 0 && index < Grades.Count)
        {
            Grades.RemoveAt(index);
        }
    }

    // Метод для поиска оценок по предмету
    public List<Grade> FindGradesBySubject(Subject subject)
    {
        return Grades.FindAll(g => g.Subject == subject);
    }

    // Метод для отображения всех оценок
    public void ShowAllGrades()
    {
        if (Grades.Count == 0)
        {
            Console.WriteLine("Оценок нет.");
            return;
        }

        Console.WriteLine($"Оценки студента {Name}:");
        for (int i = 0; i < Grades.Count; i++)
        {
            Console.WriteLine($"{i}. {Grades[i]}");
        }
    }
}