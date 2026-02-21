using Bogus;

namespace SeleniumFramework.Models.Factory
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
    }
}
