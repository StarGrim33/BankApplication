using BankApplication.DataAccessLayer.Contracts;
using BankApplication.Entities;
using BankApplication.Exceptions;

namespace BankApplication.DataAccessLayer
{
    public class TransactionDataAccessLayer : ITransactionDataAccessLayer
    {
        #region Fields
        private static List<Transaction> _transactions;
        #endregion

        #region Constructors
        static TransactionDataAccessLayer()
        {
            _transactions = new List<Transaction>();
        }
        #endregion

        #region Properties
        public List<Transaction> Transactions => _transactions;
        #endregion

        #region Methods
        public bool DeleteTransaction(Guid transactionID)
        {
            throw new NotImplementedException();
        }


        public Guid AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactions()
        {
            try
            {
                List<Transaction> transactions = new();

                Transactions.ForEach(item => transactions.Add(item.Clone() as Transaction));
                return transactions;
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
                List<Transaction> transactions = new();
                List<Transaction> filteredTransactions = Transactions.FindAll(predicate);

                filteredTransactions.ForEach(item => transactions.Add(item.Clone() as Transaction));
                return transactions;
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
                var existingTransaction = Transactions.Find(item => 
                item.TransactionID == transaction.TransactionID);

                if (existingTransaction != null)
                {
                    existingTransaction.SourceAccountID = transaction.SourceAccountID;
                    existingTransaction.DestinationAccountID = transaction.DestinationAccountID;
                    existingTransaction.Amount = transaction.Amount;
                    existingTransaction.TransactionDateTime = transaction.TransactionDateTime;

                    return true; 
                }
                else
                {
                    return false; 
                }
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
