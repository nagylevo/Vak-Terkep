using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vak_Terkep.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Vak_Terkep.Controllers
{
    public class SavedRoutesController : Controller
    {
        private readonly VakTerkepDbContext _context;
        

        public SavedRoutesController(VakTerkepDbContext context )
        {
            _context = context;
           
        }

        public async Task<IActionResult> Index()
        {
         
            var email = HttpContext.Session.GetString("email");
         

            var userInDatabase = await _context.Accounts.FirstOrDefaultAsync(x => x.emailAddress == email);

       
            Console.WriteLine(userInDatabase.Id);
         

            var savedRoutes = await _context.Saved
                .Where(s => s.AccountId == userInDatabase.Id)
                .Select(s => new SavedRoutesViewModel
                {
                    RouteId = s.Id,
                    FloorName = s.floorName,
                    TextName = s.textName,
                    BuildingName = s.buildingName,
                    Description = s.Description
                })
                .ToListAsync();

         

            return View("../Map/Routes",savedRoutes);

        }
    }
}
