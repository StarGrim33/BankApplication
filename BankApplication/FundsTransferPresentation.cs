using BankApplication.BusinessLogicLayer;
using BankApplication.BusinessLogicLayer.Contracts;
using BankApplication.Entities;

namespace BankApplication
{
    static class FundsTransferPresentation
    {
        internal static void AddTransaction()
        {
            try
            {
                Transaction transaction = new();

                ICustomerBusinessLogicLayers customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();
                ITransactionBusinessLogicLayer transactionsBusinessLogicLayer = new TransactionBusinessLogicLayer();

                AccountsPresentation.ViewAccounts();

                Console.Write("Enter the Source Account Number: ");
                long sourceAccountNumber;

                while (!long.TryParse(Console.ReadLine(), out sourceAccountNumber))
                {
                    Console.Write("Enter the Source Account Number: ");
                }

                var sourceAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                temp.AccountNumber == sourceAccountNumber).FirstOrDefault();

                if (sourceAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }

                Console.Write("Enter the Destination Account Number: ");
                long destinationAccountNumber = -1;

                bool isDestinationAccountNumberValid = false;

                while (!isDestinationAccountNumberValid)
                {
                    while (!long.TryParse(Console.ReadLine(), out destinationAccountNumber))
                    {
                        Console.Write("Enter the Destination Account Number: ");
                    }

                    if (destinationAccountNumber != sourceAccountNumber)
                    {
                        isDestinationAccountNumberValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Source account number and destination account number can't be same");
                    }
                }

                var destinationAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                temp.AccountNumber == destinationAccountNumber).FirstOrDefault();

                if (destinationAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }

                Console.WriteLine("Date of Transaction (YYYY-MM-DD hh:mm:ss tt): ");
                DateTime dateOfTransaction;

                while (!DateTime.TryParse(Console.ReadLine(), out dateOfTransaction))
                {
                    Console.WriteLine("Date of Transaction (YYYY-MM-DD hh:mm:ss tt): ");
                }

                transaction.TransactionDateTime = dateOfTransaction;

                Console.Write("Amount: ");
                decimal amount;

                while (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.Write("Amount: ");
                }

                transaction.Amount = amount;

                transaction.SourceAccountID = sourceAccount.AccountID;
                transaction.DestinationAccountID = destinationAccount.AccountID;
                Guid newGuid = transactionsBusinessLogicLayer.AddTransaction(transaction);

                Console.WriteLine("Transaction successful");
                var updatedSourceAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                temp.AccountNumber == sourceAccount.AccountNumber).FirstOrDefault();

                if (updatedSourceAccount != null)
                {
                    Console.WriteLine($"Account Balance of source account number " +
                        $"{updatedSourceAccount.AccountNumber} is: {updatedSourceAccount.Balance}.");
                }

                var updatedDestinationAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                temp.AccountNumber == destinationAccount.AccountNumber).FirstOrDefault();

                if (updatedDestinationAccount != null)
                {
                    Console.WriteLine($"Account Balance of destination account number {updatedDestinationAccount.AccountNumber} is: {updatedDestinationAccount.Balance}.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void ViewTransactions()
        {
            try
            {
                ICustomerBusinessLogicLayers customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();
                ITransactionBusinessLogicLayer transactionsBusinessLogicLayer = new TransactionBusinessLogicLayer();

                if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }

                Console.WriteLine("\n********ACCOUNT STATEMENT*************");
                AccountsPresentation.ViewAccounts();

                Console.Write("Enter the Account Number that you want to view: ");
                long accountNumberToSearch;

                while (!long.TryParse(Console.ReadLine(), out accountNumberToSearch)) { }

                var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                temp.AccountNumber == accountNumberToSearch).FirstOrDefault();

                if (existingAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }

                Console.WriteLine();
                var debitTransactions = transactionsBusinessLogicLayer.GetTransactionsByCondition(temp =>
                temp.SourceAccountID == existingAccount.AccountID).OrderBy(temp => temp.TransactionDateTime).ToList();

                var creditTransactions = transactionsBusinessLogicLayer.GetTransactionsByCondition(temp =>
                temp.DestinationAccountID == existingAccount.AccountID).OrderBy(temp => temp.TransactionDateTime).ToList();

                Console.WriteLine("Debit Transactions:");

                if (debitTransactions.Count > 0)
                {
                    Console.WriteLine($"Transaction Date, Source Account Number, Destination Account Number, Transaction Amount");

                    foreach (var transaction in debitTransactions)
                    {
                        var sourceAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                        temp.AccountID == transaction.SourceAccountID).FirstOrDefault();

                        var destinationAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                        temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();

                        if (sourceAccount != null && destinationAccount != null)
                        {
                            Console.WriteLine($"{transaction.TransactionDateTime}, {sourceAccount.AccountNumber}, " +
                                $"{destinationAccount.AccountNumber}, {transaction.Amount}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No debit transactions");
                }

                Console.WriteLine("\nCredit Transactions:");

                if (creditTransactions.Count > 0)
                {
                    Console.WriteLine($"Transaction Date, Source Account Number, Destination Account Number, Transaction Amount");

                    foreach (var transaction in creditTransactions)
                    {
                        var sourceAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                        temp.AccountID == transaction.SourceAccountID).FirstOrDefault();

                        var destinationAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                        temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();

                        if (sourceAccount != null && destinationAccount != null)
                        {
                            Console.WriteLine($"{transaction.TransactionDateTime}, {sourceAccount.AccountNumber}, " +
                                $"{destinationAccount.AccountNumber}, {transaction.Amount}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No credit transactions");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
