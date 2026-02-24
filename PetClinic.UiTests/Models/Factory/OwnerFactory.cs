using Bogus;

namespace PetClinic.UiTests.Models.Factory
{
    public class OwnerFactory:IOwnerFactory
    {
        private static readonly Faker Faker = new();

        public OwnerModel CreateDefault()
        {
            var owner = new OwnerModel()
            {
                FirstName = Faker.Name.FirstName().Replace("'", ""),
                LastName = Faker.Name.LastName().Replace("'", ""),
                Address = Faker.Address.StreetAddress(),
                City = Faker.Address.City(),
                Telephone = Faker.Random.ReplaceNumbers("##########")
            };

            return owner;
        }

        public OwnerModel CreateWith(string field, string value)
        {
            var owner = CreateDefault();

            switch (field)
            {
                case "FirstName":
                    owner.FirstName = value;
                    break;
                case "LastName":
                    owner.LastName = value;
                    break;
                case "Address":
                    owner.Address = value;
                    break;
                case "City":
                    owner.City = value;
                    break;
                case "Telephone":
                    owner.Telephone = value;
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            return owner;
        }
    }
}
