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
    }
}
