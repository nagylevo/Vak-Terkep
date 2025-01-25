using Microsoft.AspNetCore.Mvc;

namespace Vak_Terkep.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Start()
        {
            return View("Start");
        }
    }
}
