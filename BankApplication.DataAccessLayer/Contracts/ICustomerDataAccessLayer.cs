using BankApplication.Entities;
using System;

namespace BankApplication.DataAccessLayer.Contracts
{
    /// <summary>
    /// Interface that represents customers data access layer
    /// </summary>
    public interface ICustomerDataAccessLayer
    {
        /// <summary>
        /// Returns all existing customers
        /// </summary>
        /// <returns></returns>
        List<Customer> GetCustomers();
        /// <summary>
        /// Returns a set of customers that matches with specified critirea
        /// </summary>
        /// <param name="predicate"> Lambda expression that contains condition to check</param>
        /// <returns>The list of matching customers </returns>
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);

        Guid AddCustomer(Customer customer);

        bool UpdateCustomer(Customer customer);

        bool DeleteCustomer(Guid customerId);
    }
}
