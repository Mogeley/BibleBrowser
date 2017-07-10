using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BibleBrowser.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(ILogger<AdminController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}