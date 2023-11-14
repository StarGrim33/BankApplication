using BankApplication.BusinessLogicLayer.Contracts;
using BankApplication.DataAccessLayer;
using BankApplication.DataAccessLayer.Contracts;
using BankApplication.Entities;
using BankApplication.Exceptions;
using System;

namespace BankApplication.BusinessLogicLayer
{
    public class TransactionBusinessLogicLayer : ITransactionBusinessLogicLayer
    {
        #region Private Fields
        private ITransactionDataAccessLayer _transactionsDataAccessLayer;
        private AccountDataAccessLayer _accountsDataAccessLayer;
        #endregion

        #region Constructors
        public TransactionBusinessLogicLayer()
        {
            _transactionsDataAccessLayer = new TransactionDataAccessLayer();
            _accountsDataAccessLayer = new AccountDataAccessLayer();
        }
        #endregion


        #region Properties
        private ITransactionDataAccessLayer TransactionsDataAccessLayer => _transactionsDataAccessLayer;

        private IAccountDataAccessLayer AccountsDataAccessLayer => _accountsDataAccessLayer;
        #endregion


        #region Methods
        public List<Transaction> GetTransactions()
        {
            try
            {
                return TransactionsDataAccessLayer.GetTransactions();
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate)
        {
            try
            {
                return TransactionsDataAccessLayer.GetTransactionsByCondition(predicate);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddTransaction(Transaction transaction)
        {
            try
            {
                var sourceAccount = AccountsDataAccessLayer.GetAccountsByCondition(temp => 
                temp.AccountID == transaction.SourceAccountID).FirstOrDefault();

                var destinationAccount = AccountsDataAccessLayer.GetAccountsByCondition(temp => 
                temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();

                if (sourceAccount != null && destinationAccount != null)
                {
                    if (sourceAccount.Balance < transaction.Amount)
                    {
                        throw new TransactionException($"Source account has insuffient " +
                            $"funds for transaction of {transaction.Amount}");
                    }

                    sourceAccount.Balance -= transaction.Amount;
                    destinationAccount.Balance += transaction.Amount;

                    var newTransactionID = TransactionsDataAccessLayer.AddTransaction(transaction);
                    AccountsDataAccessLayer.UpdateAccount(sourceAccount);
                    AccountsDataAccessLayer.UpdateAccount(destinationAccount);

                    return newTransactionID;
                }

                throw new TransactionException("Source account or destination account number is invalid");
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            try
            {
                return TransactionsDataAccessLayer.UpdateTransaction(transaction);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteTransaction(Guid transactionID)
        {
            try
            {
                return TransactionsDataAccessLayer.DeleteTransaction(transactionID);
            }
            catch (TransactionException)
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
