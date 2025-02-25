using System;
using System.Linq;
using ClassEnrollmentGrades01.Models;
using ClassEnrollmentGrades01.Services;
using ClassEnrollmentGrades01.Services.Database;

namespace ClassEnrollmentGrades01
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dataService = new DbDataService();
            dataService.SeedInitialData();
            var menuService = new MenuService(dataService);
            menuService.ShowMainMenu();
        }
    }
}
