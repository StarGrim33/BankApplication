using BankApplication.BusinessLogicLayer.Contracts;
using BankApplication.DataAccessLayer;
using BankApplication.DataAccessLayer.Contracts;
using BankApplication.Entities;
using BankApplication.Entities.Contracts;
using BankApplication.Exceptions;

namespace BankApplication.BusinessLogicLayer
{
    public class CustomersBusinessLogicLayer : ICustomerBusinessLogicLayers
    {
        #region Private Fields
        private ICustomerDataAccessLayer _dataAccessLayer;
        #endregion

        #region Constructor
        public CustomersBusinessLogicLayer()
        {
            _dataAccessLayer = new CustomersDataAccessLayer();
        }
        #endregion

        #region Properties
        public ICustomerDataAccessLayer DataAccessLayer => _dataAccessLayer;
        #endregion

        public Guid AddCustomer(Customer customer)
        {
            try
            {
                //get all customers
                var allCustomers = DataAccessLayer.GetCustomers();
                long maxCustCode = 0;

                foreach (var item in allCustomers)
                {
                    if (item.CustomerCode > maxCustCode)
                    {
                        maxCustCode = item.CustomerCode;
                    }
                }

                if (allCustomers.Count >= 1)
                {
                    customer.CustomerCode = maxCustCode + 1;
                }
                else
                {
                    customer.CustomerCode = Configuration.Settings.BaseCustomerNo + 1;
                }

                //invoke DAL
                return _dataAccessLayer.AddCustomer(customer);
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
                return _dataAccessLayer.DeleteCustomer(customerId);
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

        public List<Customer> GetCustomers()
        {
            try
            {
                return _dataAccessLayer.GetCustomers();
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

        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                return _dataAccessLayer.GetCustomersByCondition(predicate);
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

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                return _dataAccessLayer.UpdateCustomer(customer);
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
    }
}