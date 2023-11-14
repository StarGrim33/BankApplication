using BankApplication.BusinessLogicLayer;
using BankApplication.BusinessLogicLayer.Contracts;
using BankApplication.Entities;

namespace BankApplication
{
    public class AccountsPresentation
    {
        internal static void AddAccount()
        {
            try
            {
                Account account = new();

                ICustomerBusinessLogicLayers customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();

                if (customersBusinessLogicLayer.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                Console.WriteLine("\n********ADD ACCOUNT*************");
                CustomersPresentation.ViewCustomer();

                Console.Write("Enter the Customer Code for which you want to create a new account: ");

                if (long.TryParse(Console.ReadLine(), out long customerCodeToEdit))
                {
                    var existingCustomer = customersBusinessLogicLayer.GetCustomersByCondition(temp =>
                    temp.CustomerCode == customerCodeToEdit).FirstOrDefault();

                    if (existingCustomer == null)
                    {
                        Console.WriteLine("Invalid Customer Code.\n");
                        return;
                    }

                    account.CustomerID = existingCustomer.CustomerID;

                    Guid newGuid = accountsBusinessLogicLayer.AddAccount(account);

                    List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(item =>
                    item.AccountID == newGuid);

                    if (matchingAccounts.Count >= 1)
                    {
                        Console.WriteLine("New Account Number: " + matchingAccounts[0].AccountNumber);
                        Console.WriteLine("Account Added.\n");
                    }
                    else
                    {
                        Console.WriteLine("Account Not added");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid CustomerCode.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }


        internal static void ViewAccounts()
        {
            try
            {
                ICustomerBusinessLogicLayers customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();

                List<Account> allAccounts = accountsBusinessLogicLayer.GetAccounts();

                if (allAccounts.Count == 0)
                {
                    Console.WriteLine("No accounts\n");
                    return;
                }

                Console.WriteLine("\n**********ALL ACCOUNTS*************");

                foreach (var account in allAccounts)
                {
                    Console.WriteLine("Account Number: " + account.AccountNumber);

                    var customer = customersBusinessLogicLayer.GetCustomersByCondition(temp =>
                    temp.CustomerID == account.CustomerID).FirstOrDefault();

                    if (customer != null)
                    {
                        Console.WriteLine("Customer Code: " + customer.CustomerCode);
                        Console.WriteLine("Customer Name: " + customer.CustomerName);
                    }

                    Console.WriteLine("Balance: " + account.Balance);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }


        internal static void UpdateAccount()
        {
            try
            {
                ICustomerBusinessLogicLayers customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();

                if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }

                Console.WriteLine("\n********EDIT ACCOUNT*************");
                ViewAccounts();

                Console.Write("Enter the Account Number that you want to edit: ");

                if (long.TryParse(Console.ReadLine(), out long accountCodeToEdit))
                {
                    var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp => temp.AccountNumber == accountCodeToEdit).FirstOrDefault();

                    if (existingAccount == null)
                    {
                        Console.WriteLine("Invalid Account Number.\n");
                        return;
                    }

                    Console.WriteLine();
                    CustomersPresentation.ViewCustomer();

                    Console.Write("Enter the Updated (existing) Customer Code: ");

                    if (long.TryParse(Console.ReadLine(), out long customerCodeToEdit))
                    {
                        var existingCustomer = customersBusinessLogicLayer.GetCustomersByCondition(temp =>
                        temp.CustomerCode == customerCodeToEdit).FirstOrDefault();

                        if (existingCustomer == null)
                        {
                            Console.WriteLine("Invalid Customer Code.\n");
                            return;
                        }

                        existingAccount.CustomerID = existingCustomer.CustomerID;

                        Console.Write("Balance: ");
                        existingAccount.Balance = long.Parse(Console.ReadLine());

                        bool isUpdated = accountsBusinessLogicLayer.UpdateAccount(existingAccount);

                        if (isUpdated)
                        {
                            Console.WriteLine("Account Updated.\n");
                        }
                        else
                        {
                            Console.WriteLine("Account not updated");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect customer code");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void SearchAccount()
        {
            try
            {
                ICustomerBusinessLogicLayers customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();

                if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }

                Console.WriteLine("\n********SEARCH ACCOUNT*************");
                ViewAccounts();

                Console.Write("Enter the Account Number that you want to get: ");

                if (long.TryParse(Console.ReadLine(), out long accountCodeToEdit))
                {
                    var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp =>
                    temp.AccountNumber == accountCodeToEdit).FirstOrDefault();

                    if (existingAccount == null)
                    {
                        Console.WriteLine("Invalid Account Number.\n");
                        return;
                    }

                    Console.WriteLine("Account Number: " + existingAccount.AccountNumber);

                    var customer = customersBusinessLogicLayer.GetCustomersByCondition(temp =>
                    temp.CustomerID == existingAccount.CustomerID).FirstOrDefault();

                    if (customer != null)
                    {
                        Console.WriteLine("Customer Code: " + customer.CustomerCode);
                        Console.WriteLine("Customer Name: " + customer.CustomerName);
                    }

                    Console.WriteLine("Balance: " + existingAccount.Balance);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void DeleteAccount()
        {
            try
            {
                IAccountBusinessLogicLayer accountsBusinessLogicLayer = new AccountBusinessLogicLayer();

                if (accountsBusinessLogicLayer.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }

                Console.WriteLine("\n********DELETE ACCOUNT*************");
                ViewAccounts();

                Console.Write("Enter the Account Number that you want to delete: ");

                if (long.TryParse(Console.ReadLine(), out long accountNumberToEdit))
                {
                    var existingAccount = accountsBusinessLogicLayer.GetAccountsByCondition(temp => 
                    temp.AccountNumber == accountNumberToEdit).FirstOrDefault();
                    
                    if (existingAccount == null)
                    {
                        Console.WriteLine("Invalid Account Number.\n");
                        return;
                    }

                    bool isDeleted = accountsBusinessLogicLayer.DeleteAccount(existingAccount.AccountID);

                    if (isDeleted)
                    {
                        Console.WriteLine("Account Deleted.\n");
                    }
                    else
                    {
                        Console.WriteLine("Account not deleted");
                    }
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
