using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventBrowser.Domain;
using NoDb;
using Microsoft.Extensions.Logging;

namespace BibleBrowser.Controllers
{
    public class EventController : BaseController
    {
        private readonly IBasicCommands<Event> _eventCommands;
        private readonly IBasicQueries<Event> _eventQueries;

        public EventController(
            IBasicCommands<Event> eventCommands,
            IBasicQueries<Event> eventQueries,
            ILogger<EventController> logger
            ) : base(logger)
        {
            _eventCommands = eventCommands;
            _eventQueries = eventQueries;
        }
        
        [HttpGet]
        public IActionResult List(int timeFrame)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(Event newEvent)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditEvent(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditEvent(Event newEvent)
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteEvent(Guid id)
        {
            return View();
        }
    }
}