using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BibleBrowser.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var a = 1;
            var b = 0;
            var c = a / b;

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
