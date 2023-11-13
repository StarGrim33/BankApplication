namespace BankApplication
{
    internal class Program
    {
        private const string SystemUsername = "Vlad";
        private const string SystemPassword = "Vladimir";

        static void Main(string[] args)
        {
            Console.WriteLine("************** Vladimirskiy Bank *****************");
            Console.WriteLine("::Login Page::");

            string password = null;

            Console.Write("Username: ");
            string userName = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(userName) == false && String.IsNullOrWhiteSpace(password))
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }

            if (string.Equals(userName, SystemUsername, StringComparison.OrdinalIgnoreCase) && string.Equals(password, SystemPassword, StringComparison.Ordinal))
            {
                int mainMenuChoice;

                do
                {
                    Console.WriteLine($"{Environment.NewLine}:::Main menu:::{Environment.NewLine}1. Customers{Environment.NewLine}2. Accounts{Environment.NewLine}3. Funds Transfer{Environment.NewLine}4. Funds Transfer Statement{Environment.NewLine}5. Account Statement{Environment.NewLine}0. Exit");

                    Console.Write("Enter choice: ");
                    if (int.TryParse(Console.ReadLine(), out mainMenuChoice))
                    {
                        switch (mainMenuChoice)
                        {
                            case 1:
                                CustomersMenu();
                                break;

                            case 2:
                                AccountsMenu();
                                break;

                            case 3:

                                break;

                            case 4:

                                break;

                            case 5:

                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }

                } while (mainMenuChoice != 0);
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }

            Console.WriteLine("Thank you! Visit again.");
            Console.ReadKey();
        }

        static void CustomersMenu()
        {
            int customerMenuChoice;

            do
            {
                Console.WriteLine($"{Environment.NewLine}:::Customers menu:::{Environment.NewLine}" +
                    $"1. Add Customer{Environment.NewLine}" +
                    $"2. Delete Customer{Environment.NewLine}" +
                    $"3. Update Customer{Environment.NewLine}" +
                    $"4. Search Customers{Environment.NewLine}" +
                    $"5. View Customers{Environment.NewLine}" +
                    $"0. Back to Main Menu");

                Console.Write("Enter choice: ");

                if (int.TryParse(Console.ReadLine(), out customerMenuChoice))
                {
                    switch (customerMenuChoice)
                    {
                        case 1:
                            CustomersPresentation.AddCustomer();
                            break;

                            case 2:
                            CustomersPresentation.DeleteCustomer();
                                break;

                        case 5:
                            CustomersPresentation.ViewCustomer();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

            } while (customerMenuChoice != 0);
        }

        static void AccountsMenu()
        {
            int accountsMenuChoice;

            do
            {
                Console.WriteLine($"{Environment.NewLine}:::Accounts menu:::{Environment.NewLine}1. Add Account{Environment.NewLine}2. Delete Account{Environment.NewLine}3. Update Account{Environment.NewLine}4. View Accounts{Environment.NewLine}0. Back to Main Menu");

                Console.Write("Enter choice: ");

                if (int.TryParse(Console.ReadLine(), out accountsMenuChoice))
                {

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

            } while (accountsMenuChoice != 0);
        }
    }
}
