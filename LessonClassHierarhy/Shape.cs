using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonClassHierarhy
{
    public abstract class Shape<T>
    {
        public abstract string ShapeName { get; }
        // Виртуальный метод для вычисления площади
        public abstract T Area();

        // Виртуальный метод для вычисления периметра
        public abstract T Perimeter();

        public override string ToString()
        {
            return $"Shape: {ShapeName}, Perimeter: {Perimeter()}, Area: {Area()}";
        }
    }
}
