using LessonClassHierarhy;

Rectangle<int> rect1 = new Rectangle<int>(10,20);

Console.WriteLine(rect1);

Shape<int> shape1 = rect1;

Circle<int> circle1 = new Circle<int>();

Shape<int> shape2 = circle1;

List<Shape<int>> shapes = [];

shapes.Add(circle1);
shapes.Add(rect1);

foreach (Shape<int> sh in shapes)
{
    Console.WriteLine(sh);
}

Dictionary<int, Shape<int>> dict1 = [];

dict1.Add(1, circle1);
dict1.Add(2, rect1);


