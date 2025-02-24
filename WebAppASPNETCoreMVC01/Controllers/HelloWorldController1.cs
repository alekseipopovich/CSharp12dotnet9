using Microsoft.AspNetCore.Mvc;

namespace WebAppASPNETCoreMVC01.Controllers
{
    public class HelloWorldController1 : Controller
    {
        public string Index()
        {
            return "Hello World 1";
            //return View();
        }

        public string Welcome() => "Welcome string";
    }
}
