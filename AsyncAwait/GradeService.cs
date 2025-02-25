using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleAppAsyncAwait
{
    public class GradeService
    {
        private readonly string _filePath = "grades.json";

        // Асинхронная загрузка оценок из файла
        public async Task<List<Grade>> LoadGradesAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Grade>();
            }

            try
            {
                using (FileStream fs = new FileStream(_filePath, FileMode.Open))
                {
                    return await JsonSerializer.DeserializeAsync<List<Grade>>(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading grades: {ex.Message}");
                return new List<Grade>();
            }
        }

        // Асинхронное сохранение оценок в файл
        public async Task SaveGradesAsync(List<Grade> grades)
        {
            try
            {
                using (FileStream fs = new FileStream(_filePath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, grades);
                }
                Console.WriteLine("Grades saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving grades: {ex.Message}");
            }
        }
    }
}
