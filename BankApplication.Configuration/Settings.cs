﻿namespace BankApplication.Configuration
{
    /// <summary>
    /// Project level configuration settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Customer number starts from 1001; incremented by 1
        /// </summary>
        public static long BaseCustomerNo { get; set; } = 1000;

        /// <summary>
        /// Account number starts fro, 10001; incremented by 1
        /// </summary>
        public static long BaseAccountNo { get; set; } = 10000;
    }
}