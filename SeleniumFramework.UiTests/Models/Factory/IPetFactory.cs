namespace SeleniumFramework.Models.Factory
{
    public interface IPetFactory
    {
        PetModel CreateDefault();
        PetModel CreateWith(string field, string value);
    }
}
