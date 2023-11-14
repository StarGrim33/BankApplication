using BankApplication.Entities;
using System;

namespace BankApplication.BusinessLogicLayer.Contracts
{ /// <summary>
  /// Interface that represents transactions business logic
  /// </summary>
    public interface ITransactionBusinessLogicLayer
    {
        List<Transaction> GetTransactions();

        List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate);

        Guid AddTransaction(Transaction transaction);

        bool UpdateTransaction(Transaction transaction);

        bool DeleteTransaction(Guid transactionID);
    }
}
