using BankApplication.Entities;

namespace BankApplication.BusinessLogicLayer.Contracts
{ /// <summary>
  /// Interface that represents accounts business logic
  /// </summary>
    public interface IAccountBusinessLogicLayer
    {
        List<Account> GetAccounts();

        List<Account> GetAccountsByCondition(Predicate<Account> predicate);

        Guid AddAccount(Account account);

        bool UpdateAccount(Account account);

        bool DeleteAccount(Guid accountID);
    }
}
