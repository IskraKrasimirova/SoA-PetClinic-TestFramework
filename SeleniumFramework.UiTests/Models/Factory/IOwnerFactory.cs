namespace SeleniumFramework.Models.Factory
{
    public interface IOwnerFactory
    {
        OwnerModel CreateDefault();
        OwnerModel CreateWith(
            string firstName = null, 
            string lastName = null, 
            string address = null, 
            string city = null, 
            string telephone = null);
    }
}
