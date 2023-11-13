using BankApplication.DataAccessLayer.Contracts;
using BankApplication.Entities;
using BankApplication.Exceptions;

namespace BankApplication.DataAccessLayer
{
    /// <summary>
    /// Represents DAL for bank customers
    /// </summary>
    public class CustomersDataAccessLayer : ICustomerDataAccessLayer
    {
        #region Fields
        /// <summary>
        /// The collection for test presentation, in a real-time project i will use data-bases
        /// </summary>
        private static List<Customer> _customers;
        #endregion

        #region Constructor
        static CustomersDataAccessLayer()
        {
            _customers = new List<Customer>();
        }
        #endregion

        #region Properties
        public List<Customer> Customers => _customers;
        #endregion

        #region Methods
        public Guid AddCustomer(Customer customer)
        {
            try
            {
                customer.CustomerID = Guid.NewGuid();
                _customers.Add(customer);
                return customer.CustomerID;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }      
        }

        public bool DeleteCustomer(Guid customerId)
        {
            try
            {
                if (_customers.RemoveAll(item => item.CustomerID == customerId) > 0)
                    return true;

                return false;
            }
            catch(CustomerException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }   
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customers = new();
                Customers.ForEach(customer => customers.Add(customer.Clone() as Customer));
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                List<Customer> customers = new();
                List<Customer> filteredCustomers = Customers.FindAll(predicate);
                filteredCustomers.ForEach(customer => customers.Add(customer.Clone() as Customer));
                return customers;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }        
        }

        /// <summary>
        /// Updates an existing customer`s details
        /// </summary>
        /// <param name="customer"> Customer objectc with updated details</param>
        /// <returns>Determines whether the customer is updated or not</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer));

                var existingCustomer = _customers.Find(item => item.CustomerID == customer.CustomerID);

                if (existingCustomer != null)
                {
                    existingCustomer.CustomerCode = customer.CustomerCode;
                    existingCustomer.CustomerID = customer.CustomerID;
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.CustomerAddress = customer.CustomerAddress;
                    existingCustomer.CustomerPhone = customer.CustomerPhone;
                    existingCustomer.CustomerCity = customer.CustomerCity;
                    //Object is updated
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (CustomerException)
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