class Program2
{
    // Коллекция для хранения студентов
    private static List<Student> students = new List<Student>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Показать всех студентов");
            Console.WriteLine("4. Управление оценками студента");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите опцию: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    RemoveStudent();
                    break;
                case 3:
                    ShowAllStudents();
                    break;
                case 4:
                    ManageStudentGrades();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    // Метод для добавления студента
    static void AddStudent()
    {
        Console.Write("Введите имя студента: ");
        string name = Console.ReadLine();
        students.Add(new Student(name));
        Console.WriteLine("Студент добавлен.");
    }

    // Метод для удаления студента
    static void RemoveStudent()
    {
        Console.Write("Введите индекс студента для удаления: ");
        int index = int.Parse(Console.ReadLine());

        if (index >= 0 && index < students.Count)
        {
            students.RemoveAt(index);
            Console.WriteLine("Студент удален.");
        }
        else
        {
            Console.WriteLine("Неверный индекс.");
        }
    }

    // Метод для отображения всех студентов
    static void ShowAllStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Студентов нет.");
            return;
        }

        Console.WriteLine("Список студентов:");
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i}. {students[i].Name}");
        }
    }

    // Метод для управления оценками студента
    static void ManageStudentGrades()
    {
        Console.Write("Введите индекс студента: ");
        int index = int.Parse(Console.ReadLine());

        if (index >= 0 && index < students.Count)
        {
            Student student = students[index];
            Console.WriteLine($"Выбран студент: {student.Name}");

            while (true)
            {
                Console.WriteLine("1. Добавить оценку");
                Console.WriteLine("2. Удалить оценку");
                Console.WriteLine("3. Показать все оценки");
                Console.WriteLine("4. Поиск оценок по предмету");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.Write("Выберите опцию: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddGrade(ref student);
                        break;
                    case 2:
                        RemoveGrade(ref student);
                        break;
                    case 3:
                        student.ShowAllGrades();
                        break;
                    case 4:
                        SearchGradesBySubject(ref student);
                        break;
                    case 5:
                        students[index] = student; // Сохраняем изменения
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Неверный индекс.");
        }
    }

    // Метод для добавления оценки
    static void AddGrade(ref Student student)
    {
        Console.Write("Введите предмет -  ");
        Console.Write(string.Join(", ", Enum.GetNames(typeof(Subject))));
        Console.Write(": ");

        string subjectInput = Console.ReadLine();
        if (!Enum.TryParse(subjectInput, out Subject subject))
        {
            Console.WriteLine("Неверный предмет.");
            return;
        }

        Console.Write("Введите оценку: ");
        int score = int.Parse(Console.ReadLine());

        Console.Write("Введите дату (гггг-мм-дд): ");
        DateTime date = DateTime.Parse(Console.ReadLine());

        student.AddGrade(subject, score, date);
        Console.WriteLine("Оценка добавлена.");
    }

    // Метод для удаления оценки
    static void RemoveGrade(ref Student student)
    {
        Console.Write("Введите индекс оценки для удаления: ");
        int index = int.Parse(Console.ReadLine());

        student.RemoveGrade(index);
        Console.WriteLine("Оценка удалена.");
    }

    // Метод для поиска оценок по предмету
    static void SearchGradesBySubject(ref Student student)
    {
        Console.Write("Введите предмет для поиска  -  ");
        Console.Write(string.Join(", ", Enum.GetNames(typeof(Subject))));
        Console.Write(": ");

        string subjectInput = Console.ReadLine();
        if (!Enum.TryParse(subjectInput, out Subject subject))
        {
            Console.WriteLine("Неверный предмет.");
            return;
        }

        var foundGrades = student.FindGradesBySubject(subject);
        if (foundGrades.Count == 0)
        {
            Console.WriteLine("Оценок по этому предмету нет.");
            return;
        }

        Console.WriteLine($"Оценки по предмету {subject}:");
        foreach (var grade in foundGrades)
        {
            Console.WriteLine(grade);
        }
    }
}
