using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonClassHierarhy
{
    public abstract class Shape
    {
        // Виртуальный метод для вычисления площади
        public abstract double Area();

        // Виртуальный метод для вычисления периметра
        public abstract double Perimeter();
    }
}
