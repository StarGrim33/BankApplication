namespace BankApplication.Exceptions
{
    /// <summary>
    /// Exception class that represents error raised in Customer class
    /// </summary>
    public class CustomerException : Exception
    {
        /// <summary>
        /// Constructor that initializes exception message
        /// </summary>
        /// <param name="message"></param>
        public CustomerException(string message) : base(message) { }

        /// <summary>
        /// Constructor that initializes exception message and inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomerException(string message, Exception innerException) : base(message, innerException) { }
    }
}