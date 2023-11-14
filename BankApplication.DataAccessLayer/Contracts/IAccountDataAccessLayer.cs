using BankApplication.Entities;
using System;

namespace BankApplication.DataAccessLayer.Contracts
{
    public interface IAccountDataAccessLayer
    {
        List<Account> GetAccounts();

        List<Account> GetAccountsByCondition(Predicate<Account> predicate);

        Guid AddAccount(Account account);

        bool UpdateAccount(Account account);

        bool DeleteAccount(Guid accountID);
    }
}
