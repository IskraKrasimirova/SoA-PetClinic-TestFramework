using OpenQA.Selenium;

namespace PetClinic.UiTests.Utilities
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
                    if (e is RetryException || e is StaleElementReferenceException || e is UnknownErrorException)
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
