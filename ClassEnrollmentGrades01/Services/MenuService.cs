using System;

namespace ClassEnrollmentGrades01.Services
{
    public class MenuService
    {
        private readonly CourseMenuService _courseMenu;
        private readonly StudentMenuService _studentMenu;
        private readonly EnrollmentMenuService _enrollmentMenu;

        public MenuService(IDataService dataService)
        {
            _courseMenu = new CourseMenuService(dataService);
            _studentMenu = new StudentMenuService(dataService);
            _enrollmentMenu = new EnrollmentMenuService(dataService);
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Управление курсами");
                Console.WriteLine("2. Управление студентами");
                Console.WriteLine("3. Управление оценками");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт меню: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        _courseMenu.ShowMenu();
                        break;
                    case "2":
                        _studentMenu.ShowMenu();
                        break;
                    case "3":
                        _enrollmentMenu.ShowMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
} 