namespace PetClinic.UiTests.Models.Factory
{
    public interface IOwnerFactory
    {
        OwnerModel CreateDefault();
        OwnerModel CreateWith(string field, string value);
    }
}
