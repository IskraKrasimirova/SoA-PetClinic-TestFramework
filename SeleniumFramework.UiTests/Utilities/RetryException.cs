namespace SeleniumFramework.Utilities
{
    public class RetryException : Exception
    {
        public RetryException(string message) : base(message)
        {
        }
    }
}
