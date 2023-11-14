using BankApplication.Entities;
using System;

namespace BankApplication.DataAccessLayer.Contracts
{
    public interface ITransactionDataAccessLayer
    {
        public List<Transaction> GetTransactions();

        /// <summary>
        /// Returns a set of transactions that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lamdba expression that contains condition to check</param>
        /// <returns>The list of matching transactions</returns>
        public List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate);

        /// <summary>
        /// Adds a new transaction to the existing transactions list
        /// </summary>
        /// <param name="transaction">The transaction object to add</param>
        /// <returns>Returns true, that indicates the transaction is added successfully
        /// </returns>
        public Guid AddTransaction(Transaction transaction);

        public bool UpdateTransaction(Transaction transaction);

        public bool DeleteTransaction(Guid transactionID);
    }
}
