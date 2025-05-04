using Microsoft.AspNetCore.Mvc;
using Vak_Terkep.Models;

namespace Vak_Terkep.Controllers
{
    public class SaveController : Controller
    {
        private readonly VakTerkepDbContext _context;

        public SaveController(VakTerkepDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RouteSaving(string extraData)
        {
           
            if (!string.IsNullOrEmpty(extraData))
            {
                var email = HttpContext.Session.GetString("email");

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Nincs bejelentkezett felhasználó.");
                }

                var userInDatabase = _context.Accounts.FirstOrDefault(x => x.emailAddress == email);
                if (userInDatabase == null)
                {
                    return NotFound("Felhasználó nem található.");
                }

                var routeInDatabase = _context.Routes.FirstOrDefault(x => x.textName == extraData);
                if (routeInDatabase == null)
                {
                    return NotFound("Útvonal nem található.");
                }

                var dataToBeAdded = new Save
                {
                    AccountId = userInDatabase.Id,
                    Description = routeInDatabase.description,
                    buildingName = routeInDatabase.buildingName,
                    floorName = routeInDatabase.floorName,
                    textName = routeInDatabase.textName
                };

                _context.Saved.Add(dataToBeAdded);
                await _context.SaveChangesAsync(); 

                return RedirectToAction("Start", "Map");
            }

            return BadRequest("A tartalom nem lehet üres.");
        }
    }
}
