using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

delegate string Greet(string Name);

delegate void Message();
public class SomeMethods
    {
        public static void Hello() => Console.WriteLine("Привет всем!");

        public static string GreetingName(string Name) => $"Привет {Name}!";
    }
