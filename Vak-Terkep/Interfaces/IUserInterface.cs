namespace Vak_Terkep.Interfaces
{
    public interface IUserInterface
    {
        void Add(Account accounts);
        IQueryable<Account> GetAll();
        
    }
}
