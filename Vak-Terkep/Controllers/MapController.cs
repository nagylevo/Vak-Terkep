using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Vak_Terkep.Data;
using Vak_Terkep.Implementations;
using Vak_Terkep.Interfaces;

namespace Vak_Terkep.Controllers
{
    public class MapController : Controller
    {
        private readonly IRouting _routingService;
        private readonly IDistributedCache _cache;

        public MapController(IRouting routingService, IDistributedCache distributedCache)
        {
            _routingService = routingService;
            _cache = distributedCache;
        }
        public IActionResult Start()
        {
            return View("Start");
        }

        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string textName)
        {
            await _cache.RemoveAsync("Classroomname");

            var isRouteFound = await _routingService.GivenRoute(textName);

            var routeInDb = _routingService.GetRouteByName(textName);
            if(routeInDb == null)
            {
                TempData["Message"] = "noroute";
                return RedirectToAction("Start");
            }
            return View("Map", routeInDb);
                
            
            
        }

        public IActionResult User()
        {
            return View("UserInfos");
        }

        public IActionResult Saved()
        {
            return RedirectToAction("Index","SavedRoutes");
        }

        public IActionResult RegisterUser()
        {
            return RedirectToAction("Start");
        }
    }
}
