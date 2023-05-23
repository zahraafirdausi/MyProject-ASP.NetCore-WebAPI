using MyProject.Model;
namespace MyProject.Repository.Interface
{
    public interface IAccountRepository
    {
        //Get All data
        IEnumerable<Account> Get();
        Account Get(string NIK); //Get data by NIK
        //int Insert(Account account);
        int Update(Account account);
        int Delete(string NIK);

    }
}
