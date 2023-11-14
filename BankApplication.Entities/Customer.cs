using BankApplication.Entities.Contracts;
using BankApplication.Exceptions;
using System.Threading.Channels;

namespace BankApplication.Entities
{
    /// <summary>
    /// Represents customer of the bank
    /// </summary>
    public class Customer : ICustomer, ICloneable
    {
        #region Private fields
        private Guid _id;
        private long _code;
        private string _name;
        private string _address;
        private string _phone;
        private string _city;
        private string _country;
        #endregion

        #region Public Properties
        /// <summary>
        /// Guid of Customer for Unique identification
        /// </summary>
        public Guid CustomerID { get => _id; set => _id = value; }

        public string CustomerName
        {
            get => _name;
            set
            {
                if (value.Length <= 40 && string.IsNullOrWhiteSpace(value) == false)
                    _name = value;
                else
                    throw new CustomerException("Customer name should not be null and less than 40 characters long");
            }
        }

        public string CustomerAddress { get => _address; set => _address = value; }

        public string CustomerPhone 
        { 
            get => _phone; 
            set
            {
                if (value.Length == 10)
                    _phone = value;
                else
                    throw new CustomerException("Mobile number should be 10-digit number and without +7(+8)");
            }
        }

        public string CustomerCity { get => _city; set => _city = value; }

        public string CustomerCountry { get => _country; set => _country = value; }

        public long CustomerCode
        {
            get => _code;
            set
            {
                if (value > 0)
                    _code = value;
                else
                    throw new CustomerException("Customer code should be positive only");
            }
        }
        #endregion

        #region Methods
        public object Clone()
        {
            return new Customer() 
            { 
                CustomerName = this.CustomerName, 
                CustomerID = this.CustomerID, 
                CustomerAddress = this.CustomerAddress, 
                CustomerPhone = this.CustomerPhone, 
                CustomerCity = this.CustomerCity, 
                CustomerCountry = this.CustomerCountry,
                CustomerCode = this.CustomerCode
            };
        }

        public override string ToString()
        {
             string info = $"\nCustomer code: {CustomerCode}, \ncustomer name: {CustomerName}, \ncustomer address: {CustomerAddress}" +
                $", \ncustomer phone: {CustomerPhone}, \ncustomer city: {CustomerCity}, \ncustomer country: {CustomerCountry}";

            return info;
        }
        #endregion
    }
}
