using Microsoft.EntityFrameworkCore;
using Vak_Terkep.Interfaces;
using Vak_Terkep.Models;

namespace Vak_Terkep.Implementations
{
    public class DatabaseUserManager : IUserInterface
    {
        private VakTerkepDbContext dbcontext;
        public DatabaseUserManager(VakTerkepDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void Add(Account accounts)
        {
            dbcontext.Accounts.Add(accounts);
            dbcontext.SaveChanges();
        }

        public IQueryable<Account> GetAll()
        {
            return dbcontext.Accounts.AsQueryable();
        }
      
    }
}
