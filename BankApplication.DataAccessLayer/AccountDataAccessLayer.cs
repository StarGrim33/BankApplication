using BankApplication.DataAccessLayer.Contracts;
using BankApplication.Entities;
using BankApplication.Exceptions;

namespace BankApplication.DataAccessLayer
{
    public class AccountDataAccessLayer : IAccountDataAccessLayer
    {
        #region Private Fields
        private List<Account> _accounts;
        #endregion

        #region Constructors
        public AccountDataAccessLayer()
        {
            _accounts = new List<Account>()
            {
                new Account() { AccountID = Guid.Parse("DFBED28D-87D1-4295-AE9F-4EB0F7534C8B"), AccountNumber = 1001, Balance = 1000, CustomerID = Guid.Parse("8396157F-1C34-45C5-9925-3E82D1439EAD") },
                new Account() { AccountID = Guid.Parse("68319657-1FCF-49CC-9193-C4442F55AD28"), AccountNumber = 10002, Balance = 500, CustomerID = Guid.Parse("8C12BEA9-8FB0-4744-8422-1996533805E8") },
            };
        }
        #endregion

        #region Properties
        public List<Account> Accounts => _accounts;
        #endregion

        #region Methods
        public Guid AddAccount(Account account)
        {
            try
            {
                //generate new guid
                account.AccountID = Guid.NewGuid();
                _accounts.Add(account);
                return account.AccountID;
            }
            catch (AccountException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool DeleteAccount(Guid accountID)
        {
            try
            {
                if(Accounts.RemoveAll(item => item.AccountID == accountID) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> GetAccounts()
        {
            try
            {
                List<Account> accountList = new();

                Accounts.ForEach(item => accountList.Add(item.Clone() as Account));
                return accountList;
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> GetAccountsByCondition(Predicate<Account> predicate)
        {
            try
            {
                List<Account> accountsList = new List<Account>();

                List<Account> filteredAccounts = Accounts.FindAll(predicate);

                filteredAccounts.ForEach(item => accountsList.Add(item.Clone() as Account));
                return accountsList;
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAccount(Account account)
        {
            try
            {
                Account existingAccount = Accounts.Find(item => item.AccountID == account.AccountID);

                if(existingAccount != null)
                {
                    existingAccount.AccountID = account.AccountID;
                    existingAccount.AccountNumber = account.AccountNumber;
                    existingAccount.CustomerID = account.CustomerID;
                    existingAccount.Balance = account.Balance;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
