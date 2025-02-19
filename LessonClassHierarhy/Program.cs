using LessonClassHierarhy;

Rectangle rect1 = new Rectangle();

Shape shape1 = rect1;

Circle circle1 = new Circle();

Shape shape2 = circle1;

List<Shape> shapes = [];

shapes.Add(circle1);
shapes.Add(rect1);

foreach (Shape sh in shapes)
{
    Console.WriteLine(sh.ToString());
}

Dictionary<int, Shape> dict1 = [];

dict1.Add(1, circle1);
dict1.Add(2, rect1);


