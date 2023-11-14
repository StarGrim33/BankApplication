using BankApplication.Entities.Contracts;
using System;

namespace BankApplication.Entities
{
    public class Account : ICloneable, IAccount
    {
        #region Fields
        private Guid _customerID;
        private Guid _accountID;
        private long _accountNumber;
        private decimal _balance;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Account class.
        /// </summary>
        public Account()
        {
            // Initialize account properties
            CustomerID = Guid.Empty;
            AccountID = Guid.Empty;
            AccountNumber = 0L;
            Balance = 0.0m;
        }

        /// <summary>
        /// Initializes a new instance of the Account class with the specified customer ID, account ID, account number, and balance.
        /// </summary>
        /// <param name="customerID">The customer ID associated with the account.</param>
        /// <param name="accountID">The account ID.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="balance">The balance in the account.</param>
        public Account(Guid customerID, Guid accountID, long accountNumber, decimal balance)
        {
            CustomerID = customerID;
            AccountID = accountID;
            AccountNumber = accountNumber;
            Balance = balance;
        }
        #endregion
        #region Properties
        public Guid CustomerID
        {
            get => _customerID;
            set => _customerID = value;
        }

        public Guid AccountID
        {
            get => _accountID;
            set => _accountID = value;
        }

        public long AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Account number negative.");
                }
                _accountNumber = value;
            }
        }

        public decimal Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Balance cannot be negative.");
                }

                _balance = value;
            }
        }
        #endregion

        #region Methods
        public object Clone()
        {
            return new Account()
            {
                AccountID = AccountID,
                AccountNumber = AccountNumber,
                Balance = Balance,
                CustomerID = CustomerID,
            };
        }
        #endregion
    }
}
