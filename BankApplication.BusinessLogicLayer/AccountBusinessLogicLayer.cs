using BankApplication.BusinessLogicLayer.Contracts;
using BankApplication.DataAccessLayer;
using BankApplication.DataAccessLayer.Contracts;
using BankApplication.Entities;
using BankApplication.Exceptions;
using System;
using System.Collections.Generic;

namespace BankApplication.BusinessLogicLayer
{
    public class AccountBusinessLogicLayer : IAccountBusinessLogicLayer
    {
        #region Private Fields
        private IAccountDataAccessLayer _accountDataAccessLayer;
        #endregion

        #region Constructors
        public AccountBusinessLogicLayer()
        {
            _accountDataAccessLayer = new AccountDataAccessLayer();
        }
        #endregion

        #region Properties
        public IAccountDataAccessLayer AccountDataAccessLayer => _accountDataAccessLayer;
        #endregion

        #region Methods
        public Guid AddAccount(Account account)
        {
            try
            {
                List<Account> allAccounts = AccountDataAccessLayer.GetAccounts();
                long maxAccountNumber = 0;

                foreach (var item in allAccounts)
                {
                    if (item.AccountNumber > maxAccountNumber)
                    {
                        maxAccountNumber = item.AccountNumber;
                    }
                }

                if (allAccounts.Count >= 1)
                {
                    account.AccountNumber = maxAccountNumber + 1;
                }
                else
                {
                    account.AccountNumber = Configuration.Settings.BaseAccountNo + 1;
                }

                account.Balance = 0.0M;

                return AccountDataAccessLayer.AddAccount(account);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteAccount(Guid accountID)
        {
            try
            {
                return AccountDataAccessLayer.DeleteAccount(accountID);
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

        public List<Account> GetAccounts()
        {
            try
            {
                return AccountDataAccessLayer.GetAccounts();
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
                return AccountDataAccessLayer.GetAccountsByCondition(predicate);
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
                return AccountDataAccessLayer.UpdateAccount(account);
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
