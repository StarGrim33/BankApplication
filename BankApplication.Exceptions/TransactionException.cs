namespace BankApplication.Exceptions
{
    /// <summary>
    /// Exception class that represents error raised in Transaction class
    /// </summary>
    public class TransactionException : Exception
    {
        /// <summary>
        /// Constructor that initializes exception message
        /// </summary>
        /// <param name="message">exception message</param>
        public TransactionException(string message) : base(message) { }

        /// <summary>
        /// Constructor that initializes exception message and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public TransactionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
