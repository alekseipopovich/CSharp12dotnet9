using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonClassHierarhy
{
    class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle() : this(10.0) { }

        public Circle(double radius)
        {
            Radius = radius;
        }

        // Переопределение метода для вычисления площади круга
        public override double Area() => Math.PI * Radius * Radius;

        // Переопределение метода для вычисления периметра (длины окружности)
        public override double Perimeter() => 2 * Math.PI * Radius;
    }
}
