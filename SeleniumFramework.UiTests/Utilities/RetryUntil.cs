using OpenQA.Selenium;

namespace SeleniumFramework.Utilities
{
    public static class Retry
    {
        public static void Until(Action action, int retryNumber = 3, int waitInMilliseconds = 500)
        {
            while (retryNumber != 0)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception e )
                {
                    if (e is RetryException || e is StaleElementReferenceException)
                    {

                        retryNumber--;
                        Thread.Sleep(waitInMilliseconds);

                        continue;
                    }
                    else
                        throw;
                }

                break;
            }
        }
    }
}
