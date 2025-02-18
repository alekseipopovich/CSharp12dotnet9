using ClassEnrollment.Entities;
using ClassEnrolment.Logic;
using System;
using System.Collections.Generic;

namespace ClassEnrolment
{
    class Program
    {
        private static List<Course> courses = new List<Course>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Добавить курс");
                Console.WriteLine("2. Записать студента на курс");
                Console.WriteLine("3. Показать список студентов на курсе");
                Console.WriteLine("4. Удалить студента из курса");
                Console.WriteLine("5. Показать список курсов");
                Console.WriteLine("6. Удалить курс");
                Console.WriteLine("7. Показать список всех студентов на всех курсах");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите опцию: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Actions.AddCourse();
                        break;
                    case 2:
                        Actions.EnrollStudent();
                        break;
                    case 3:
                        Actions.ShowStudents();
                        break;
                    case 4:
                        Actions.RemoveStudent();
                        break;
                    case 5:
                        Actions.ShowCourses();
                        break;
                    case 6:
                        Actions.RemoveCourse();
                        break;
                    case 7:
                        Actions.ShowAllStudents();
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }

}