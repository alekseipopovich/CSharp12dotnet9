namespace ConsoleAppAsyncAwait;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var gradeService = new GradeService();

        // Асинхронная загрузка оценок
        List<Grade> grades = await gradeService.LoadGradesAsync();
        Console.WriteLine("Загрузка оценок:");

        foreach (var grade in grades)
        {
            Console.WriteLine($"{grade.StudentName} - {grade.Subject}: {grade.Score}");
        }

        // Добавление новой оценки
        grades.Add(new Grade { StudentName = "Вася", Subject = "Математика", Score = 4.5 });

        // Асинхронное сохранение оценок
        await gradeService.SaveGradesAsync(grades);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}