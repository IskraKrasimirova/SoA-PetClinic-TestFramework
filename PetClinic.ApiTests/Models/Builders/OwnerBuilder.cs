using Bogus;
using PetClinic.ApiTests.Models.Dtos;

namespace PetClinic.ApiTests.Models.Builders
{
    public class OwnerBuilder
    {
        private static readonly Faker Faker = new();
        private readonly OwnerDto _ownerDto;

        public OwnerBuilder()
        {
            _ownerDto = new OwnerDto();
        }

        public OwnerBuilder CreateWithDefaultValues()
        {
            _ownerDto.FirstName = Faker.Name.FirstName().Replace("'", "");
            _ownerDto.LastName = Faker.Name.LastName().Replace("'", "");
            _ownerDto.Address = Faker.Address.StreetAddress();
            _ownerDto.City = Faker.Address.City();
            _ownerDto.Telephone = Faker.Random.ReplaceNumbers("##########");

            return this;
        }

        public OwnerDto Build()
        {
            return _ownerDto;
        }
    }
}
