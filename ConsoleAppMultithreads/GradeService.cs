using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMultithreads
{
    public class GradeService
    {
        // Метод для расчета среднего балла студента
        public double CalculateAverageScore(List<Grade> grades, string studentName)
        {
            double totalScore = 0;
            int count = 0;

            foreach (var grade in grades)
            {
                if (grade.StudentName == studentName)
                {
                    totalScore += grade.Score;
                    count++;
                }
            }

            return count > 0 ? totalScore / count : 0;
        }
    }
}
