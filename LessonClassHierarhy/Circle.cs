using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LessonClassHierarhy
{
    class Circle<T> : Shape<T>, IMovable where T : INumber<T>
    {
        public override string ShapeName => "Окружность";
        public string Name { get; set; }
        public T Radius { get; set; }

        public Circle() : this(T.CreateChecked(10)) { }

        public Circle(T radius)
        {
            Radius = radius;
        }

        // Переопределение метода для вычисления площади круга
        public override T Area() => T.CreateChecked(Math.PI) * Radius * Radius;

        // Переопределение метода для вычисления периметра (длины окружности)
        public override T Perimeter() => T.CreateChecked(2) * T.CreateChecked(Math.PI) * Radius;

        public void Move()
        {
            Console.WriteLine("Circle move");
        }
    }
}
