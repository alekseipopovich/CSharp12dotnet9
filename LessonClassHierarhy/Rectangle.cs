using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LessonClassHierarhy
{
    public class Rectangle<T> : Shape<T>, IMovable where T : INumber<T>
    {
        public override string ShapeName => "Прямоугольник";
        private T Width { get; set; }
        private T Height { get; set; }

        public Rectangle(T width, T height)
        {
            Width = width;
            Height = height;
        }

        public override T Area() => Width * Height;
        public override T Perimeter() => T.CreateChecked(2) * (Width + Height);

        public void Move()
        {
            Console.WriteLine("Rectangle move");
        }

    }
}
