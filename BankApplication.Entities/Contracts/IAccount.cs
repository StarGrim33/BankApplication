using System;

namespace BankApplication.Entities.Contracts
{    /// <summary>
     /// Represents interface of account entity
     /// </summary>
    public interface IAccount
    {
        #region Properties
        Guid AccountID { get; set; }

        long AccountNumber { get; set; }

        decimal Balance { get; set; }

        Guid CustomerID { get; set; }
        #endregion
    }
}
