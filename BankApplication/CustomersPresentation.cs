using BankApplication.BusinessLogicLayer;
using BankApplication.BusinessLogicLayer.Contracts;
using BankApplication.Entities;

namespace BankApplication
{
    static class CustomersPresentation
    {
        /// <summary>
        /// I dont catch here any exceptions cause its a presentation layer (top layer), i do that in customer class
        /// </summary>

        #region Methods
        internal static void AddCustomer()
        {
            try
            {
                Customer customer = new();
                Console.WriteLine("\n********Add customer********");

                Console.WriteLine("Customer name: ");
                customer.CustomerName = Console.ReadLine();

                Console.WriteLine("Address: ");
                customer.CustomerAddress = Console.ReadLine();

                Console.WriteLine("City: ");
                customer.CustomerCity = Console.ReadLine();

                Console.WriteLine("Country: ");
                customer.CustomerCountry = Console.ReadLine();

                Console.WriteLine("Mobile number without +7 or +8, only 10 numbers: ");
                customer.CustomerPhone = Console.ReadLine();

                ICustomerBusinessLogicLayers customerBusinessLogicLayers = new CustomersBusinessLogicLayer();
                Guid newGuid = customerBusinessLogicLayers.AddCustomer(customer);

                List<Customer> matchingCustomers = customerBusinessLogicLayers.GetCustomersByCondition(x => x.CustomerID == newGuid);

                if (matchingCustomers.Count > 0)
                {
                    Console.WriteLine($"New customer code: {matchingCustomers[0].CustomerCode}");
                    Console.WriteLine(newGuid);
                    Console.WriteLine("Customer added\n");
                }
                else
                {
                    Console.WriteLine("Customer not added");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType);
            }
        }

        internal static void ViewCustomer()
        {
            try
            {
                ICustomerBusinessLogicLayers customerBusinessLogicLayers = new CustomersBusinessLogicLayer();
                List<Customer> allCustomers = customerBusinessLogicLayers.GetCustomers();

                if (allCustomers.Count == 0)
                {
                    Console.WriteLine("No customers\n");
                    return;
                }

                Console.WriteLine("\n**********ALL CUSTOMERS*************");
                foreach (Customer customer in allCustomers)
                {
                    Console.WriteLine(customer.ToString());
                }

                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void DeleteCustomer()
        {
            try
            {
                ICustomerBusinessLogicLayers customerBusinessLogicLayers = new CustomersBusinessLogicLayer();
                ViewCustomer();
                Console.WriteLine("Which customer do you want to delete, enter CustomerCode? : ");

                if (customerBusinessLogicLayers.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                if (long.TryParse(Console.ReadLine(), out long customerCode))
                {
                    var matchingCustomers = customerBusinessLogicLayers.GetCustomersByCondition(item => item.CustomerCode == customerCode);

                    if (matchingCustomers.Count > 0)
                    {
                        Customer customerToDelete = matchingCustomers[0];
                        customerBusinessLogicLayers.DeleteCustomer(customerToDelete.CustomerID);

                        Console.WriteLine("Customer deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Customer not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid CustomerCode.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType);
            }
        }

        internal static void SearchCustomer()
        {
            try
            {
                ICustomerBusinessLogicLayers customerBusinessLogicLayers = new CustomersBusinessLogicLayer();

                if(customerBusinessLogicLayers.GetCustomers().Count == 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                Console.WriteLine("\n********SEARCH CUSTOMER*************");
                ViewCustomer();

                Console.Write("Enter the Customer Code that you want to get: ");

                if (long.TryParse(Console.ReadLine(), out long customerCodeUserInput))
                {
                    var existingCustomer = customerBusinessLogicLayers.GetCustomersByCondition(item 
                        => item.CustomerCode == customerCodeUserInput).FirstOrDefault();

                    if (existingCustomer == null)
                    {
                        Console.WriteLine("Invalid Customer Code.\n");
                        return;
                    }

                    existingCustomer?.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void UpdateCustomer()
        {
            try
            {
                ICustomerBusinessLogicLayers customerBusinessLogicLayers = new CustomersBusinessLogicLayer();

                if (customerBusinessLogicLayers.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }

                Console.WriteLine("\n********EDIT CUSTOMER*************");
                ViewCustomer();

                Console.Write("Enter the Customer Code that you want to edit: ");

                if (long.TryParse(Console.ReadLine(), out long customerCodeUserInput))
                {
                    var existingCustomer = customerBusinessLogicLayers.GetCustomersByCondition(item
                        => item.CustomerCode == customerCodeUserInput).FirstOrDefault();

                    if (existingCustomer == null)
                    {
                        Console.WriteLine("Invalid Customer Code.\n");
                        return;
                    }

                    Console.WriteLine("NEW CUSTOMER DETAILS:");
                    Console.Write("Customer Name: ");
                    existingCustomer.CustomerName = Console.ReadLine();
                    Console.Write("Address: ");
                    existingCustomer.CustomerAddress = Console.ReadLine();
                    Console.Write("City: ");
                    existingCustomer.CustomerCity = Console.ReadLine();
                    Console.Write("Country: ");
                    existingCustomer.CustomerCountry = Console.ReadLine();
                    Console.Write("Mobile: ");
                    existingCustomer.CustomerPhone = Console.ReadLine();


                    bool isUpdated = customerBusinessLogicLayers.UpdateCustomer(existingCustomer);

                    if (isUpdated)
                    {
                        Console.WriteLine("Customer Updated.\n");
                    }
                    else
                    {
                        Console.WriteLine("Customer not updated");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
