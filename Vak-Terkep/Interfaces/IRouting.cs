using Vak_Terkep.Models;
namespace Vak_Terkep.Interfaces
{
    public interface IRouting
    {
        IQueryable<Route> GetAllRoutes();
        Task<bool> GivenRoute(string variable);
        Task CacheData(string key, string value, int time);
        Route GetRouteByName(string routeName);
    }
}
