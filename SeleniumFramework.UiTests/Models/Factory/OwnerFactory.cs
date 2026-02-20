using Bogus;

namespace SeleniumFramework.Models.Factory
{
    public class OwnerFactory:IOwnerFactory
    {
        private static readonly Faker Faker = new();

        public OwnerModel CreateDefault()
        {
            var owner = new OwnerModel()
            {
                FirstName = Faker.Name.FirstName(),
                LastName = Faker.Name.LastName(),
                Address = Faker.Address.StreetAddress(),
                City = Faker.Address.City(),
                Telephone = Faker.Random.ReplaceNumbers("##########")
            };

            return owner;
        }

        public OwnerModel CreateWith(
            string firstName = null, 
            string lastName = null, 
            string address = null, 
            string city = null, 
            string telephone = null)
        {
            var owner = CreateDefault();

            if (firstName != null) owner.FirstName = firstName;
            if (lastName != null) owner.LastName = lastName;
            if (address != null) owner.Address = address;
            if (city != null) owner.City = city;
            if (telephone != null) owner.Telephone = telephone;

            return owner;
        }
    }
}
