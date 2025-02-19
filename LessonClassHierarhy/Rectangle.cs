using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonClassHierarhy
{
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle() : this(10.0, 10.0) { }
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double Area() => Width * Height;
        public override double Perimeter() => 2 * (Width + Height);
       
    }
}
