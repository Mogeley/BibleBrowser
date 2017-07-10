using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventBrowser.Domain;
using NoDb;

namespace BibleBrowser.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBasicCommands<Event> _eventCommands;
        private readonly IBasicQueries<Event> _eventQueries;

        public HomeController(
            IBasicCommands<Event> eventCommands,
            IBasicQueries<Event> eventQueries,
            ILogger<HomeController> logger
            ) : base(logger)
        {
            _eventCommands = eventCommands;
            _eventQueries = eventQueries;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorTest()
        {
            var a = 1;
            var b = 0;
            var c = a / b;

            return View();
        }

        public IActionResult AddEventTest()
        {
            var eventCount = _eventQueries.GetAllAsync("BibleBrowserTest").Result.Count() + 1;

            var e = new Event("Test Event " + eventCount.ToString());

            _eventCommands.CreateAsync("BibleBrowserTest", e.Id.ToString(), e);

            var events =  _eventQueries.GetAllAsync("BibleBrowserTest").Result;

            return View(events);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
