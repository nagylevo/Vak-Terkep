
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Vak_Terkep.Interfaces;
using Vak_Terkep.Models;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.EntityFrameworkCore;

namespace Vak_Terkep.Implementations
{
    public class RoutingService : IRouting
    {
        private readonly IDistributedCache _cache;
        private readonly VakTerkepDbContext _context;
        public RoutingService(IDistributedCache cache, VakTerkepDbContext context) {
            _cache = cache;
            _context = context;

        }

        public IQueryable<Route> GetAllRoutes()
        {
            return _context.Routes.AsQueryable();
        }

        public async Task<bool> GivenRoute(string variable)
        {
            var routeInDatabase = _context.Routes.FirstOrDefault(x => x.textName == variable);
            if (routeInDatabase == null)
            {
                return false;
            }

            await CacheData("Classroomname", variable, 1);
            return true;
        }
        public async Task CacheData(string key, string value, int time) 
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(time)
            };
            await _cache.SetStringAsync(key, value, options);
        }

        public Route GetRouteByName(string routeName)
        {
            if (string.IsNullOrEmpty(routeName))
            {
                return null;
            }
            return _context.Routes.FirstOrDefault(r => r.textName == routeName);
        }
    }
}

