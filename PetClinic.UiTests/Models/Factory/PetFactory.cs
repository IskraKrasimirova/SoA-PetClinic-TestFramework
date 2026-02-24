using Bogus;

namespace PetClinic.UiTests.Models.Factory
{
    public class PetFactory : IPetFactory
    {
        private static readonly Faker Faker = new();
        private static readonly List<string> ValidTypes = ["bird", "cat", "dog", "hamster", "lizard", "snake"];

        public PetModel CreateDefault()
        {
            var pet = new PetModel()
            {
                Name = Faker.Name.FirstName().Replace("'", ""),
                BirthDate = Faker.Date.Past(10).ToString("yyyy-MM-dd"),
                Type = Faker.PickRandom(ValidTypes)
            };

            return pet;
        }

        public PetModel CreateWith(string field, string value)
        {
            var pet = CreateDefault();

            switch (field)
            {
                case "Name":
                    pet.Name = value;
                    break;
                case "BirthDate":
                    pet.BirthDate = value;
                    break;
                case "Type":
                    pet.Type = value;
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            return pet;
        }
    }
}
