using FluentAssertions;
using FluentAssertions.Execution;
using PetClinic.ApiTests.Apis;
using PetClinic.ApiTests.Models.Builders;
using PetClinic.ApiTests.Models.Dtos;
using PetClinic.ApiTests.Utils;
using Reqnroll;

namespace SeleniumFramework.ApiTests.Steps
{
    [Binding]
    public class OwnersApiSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OwnersApi _ownersApi;
        private readonly OwnerBuilder _ownerBuilder;

        public OwnersApiSteps(ScenarioContext scenarioContext, OwnersApi ownersApi, OwnerBuilder ownerBuilder)
        {
            this._scenarioContext = scenarioContext;
            this._ownersApi = ownersApi;
            this._ownerBuilder = ownerBuilder;
        }

        [Given("I make a get request to owners endpoint")]
        public void GivenIMakeAGetRequestToOwnersEndpoint()
        {
            var response = _ownersApi.GetOwners();

            _scenarioContext.Add(ContextConstants.StatusCode, (int)response.StatusCode);
            _scenarioContext.Add(ContextConstants.ContentType, response.ContentType);
            _scenarioContext.Add(ContextConstants.OwnersResponse, response.Data);
        }

        [Given("I select an existing owner from the response")]
        public void GivenISelectAnExistingOwnerFromTheResponse()
        {
            var ownersList = _scenarioContext.Get<IReadOnlyCollection<OwnerDto>>(ContextConstants.OwnersResponse);
            ownersList.Should().NotBeNullOrEmpty();

            var random = new Random();
            var selectedOwner = ownersList.ElementAt(random.Next(ownersList.Count));
            selectedOwner.Should().NotBeNull();

            _scenarioContext.Add(ContextConstants.SelectedOwner, selectedOwner);
        }

        [Given("I create a new owner successfully")]
        public void GivenICreateANewOwnerSuccessfully()
        {
            var newOwner = _ownerBuilder.CreateWithDefaultValues().Build();
            var response = _ownersApi.CreateOwner(newOwner);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            _scenarioContext[ContextConstants.CreatedOwner] = response.Data;
        }


        [When("I make a post request to create a pet for the selected owner with valid data")]
        public void WhenIMakeAPostRequestToCreateAPetForTheSelectedOwnerWithValidData()
        {
            var selectedOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.SelectedOwner);
            var selectedPetType = _scenarioContext.Get<PetTypeDto>(ContextConstants.SelectedPetType);

            var newPet = new PetDto
            {
                Name = "Buddy",
                BirthDate = DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd"),
                Type = selectedPetType
            };

            _scenarioContext[ContextConstants.NewPet] = newPet;

            var response = _ownersApi.CreatePetForOwner(selectedOwner.Id, newPet);

            _scenarioContext[ContextConstants.StatusCode] = (int)response.StatusCode;
            _scenarioContext[ContextConstants.ContentType] = response.ContentType;
            _scenarioContext[ContextConstants.CreatedPet] = response.Data;
        }

        [When("I make a post request to create a pet for the newly created owner with valid data")]
        public void WhenIMakeAPostRequestToCreateAPetForTheNewlyCreatedOwnerWithValidData()
        {
            var createdOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.CreatedOwner);
            var selectedPetType = _scenarioContext.Get<PetTypeDto>(ContextConstants.SelectedPetType);

            var newPet = new PetDto
            {
                Name = "Buddy",
                BirthDate = DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd"),
                Type = selectedPetType
            };

            _scenarioContext[ContextConstants.NewPet] = newPet;

            var response = _ownersApi.CreatePetForOwner(createdOwner.Id, newPet);

            _scenarioContext[ContextConstants.StatusCode] = (int)response.StatusCode;
            _scenarioContext[ContextConstants.ContentType] = response.ContentType;
            _scenarioContext[ContextConstants.CreatedPet] = response.Data;
        }

        [Then("owners response should contain non-empty list of owners")]
        public void ThenOwnersResponseShouldContainNon_EmptyListOfOwners()
        {
            var ownersList = _scenarioContext.Get<IReadOnlyCollection<OwnerDto>>(ContextConstants.OwnersResponse);

            ownersList.Should().NotBeNullOrEmpty();
        }

        [Then("each owner in the list should have valid data")]
        public void ThenEachOwnerInTheListShouldHaveValidData()
        {
            var ownersList = _scenarioContext.Get<IReadOnlyCollection<OwnerDto>>(ContextConstants.OwnersResponse);

            foreach (var owner in ownersList)
            {
                using (new AssertionScope())
                {
                    owner.Id.Should().BeGreaterThan(0, "Owner ID should be a positive number");
                    owner.FirstName.Should().NotBeNullOrWhiteSpace("FirstName is required");
                    owner.LastName.Should().NotBeNullOrWhiteSpace("LastName is required");
                    owner.Address.Should().NotBeNullOrWhiteSpace("Address is required");
                    owner.City.Should().NotBeNullOrWhiteSpace("City is required");
                    owner.Telephone.Should().NotBeNullOrWhiteSpace("Telephone is required");

                    if (owner.Pets != null && owner.Pets.Count != 0)
                    {
                        foreach (var pet in owner.Pets)
                        {
                            pet.Id.Should().BeGreaterThan(0);
                            pet.Name.Should().NotBeNullOrWhiteSpace("Pet Name is required");
                            pet.BirthDate.Should().NotBeNullOrWhiteSpace("Pet BirthDate is required");

                            pet.Type.Should().NotBeNull("Pet Type is required");
                            pet.Type.Id.Should().BeGreaterThan(0);
                            pet.Type.Name.Should().NotBeNullOrWhiteSpace();

                            pet.Visits.Should().NotBeNull();

                            pet.OwnerId.Should().Be(owner.Id, "Pet's OwnerId should match the Owner's Id");
                        }
                    }
                }
            }
        }

        [Then("the created pet response should contain valid pet data")]
        public void ThenTheCreatedPetResponseShouldContainValidPetData()
        {
            var expectedPet = _scenarioContext.Get<PetDto>(ContextConstants.NewPet);
            var actualPet = _scenarioContext.Get<PetDto>(ContextConstants.CreatedPet);
            var expectedPetType = _scenarioContext.Get<PetTypeDto>(ContextConstants.SelectedPetType);

            using (new AssertionScope()) 
            {
                actualPet.Should().NotBeNull("Response must contain pet data");
                actualPet.Id.Should().BeGreaterThan(0, "Pet must have a valid ID");
                actualPet.Name.Should().Be(expectedPet.Name, "Pet name must match the input");
                actualPet.Type.Should().NotBeNull();
                actualPet.Type.Id.Should().Be(expectedPetType.Id, "Pet type ID must match the input");

                actualPet.Should().BeEquivalentTo(
                expectedPet,
                options => options
                    .Excluding(p => p.Id)
                    .Excluding(p => p.OwnerId)
                );
            }
        }

        [Then("I make a get request to retrieve the created pet by its ID and owner ID")]
        public void ThenIMakeAGetRequestToRetrieveTheCreatedPetByItsIDAndOwnerID()
        {
            var selectedOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.SelectedOwner);
            var createdPet = _scenarioContext.Get<PetDto>(ContextConstants.CreatedPet);

            var response = _ownersApi.GetPetById(selectedOwner.Id, createdPet.Id);

            _scenarioContext[ContextConstants.StatusCode] = (int)response.StatusCode;
            _scenarioContext[ContextConstants.ContentType] = response.ContentType;
            _scenarioContext[ContextConstants.OwnersResponse] = response.Data;
        }

        [Then("I make a get request to retrieve the created pet by its ID and created owner ID")]
        public void ThenIMakeAGetRequestToRetrieveTheCreatedPetByItsIDAndCreatedOwnerID()
        {
            var createdOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.CreatedOwner);
            var createdPet = _scenarioContext.Get<PetDto>(ContextConstants.CreatedPet);

            var response = _ownersApi.GetPetById(createdOwner.Id, createdPet.Id);

            _scenarioContext[ContextConstants.StatusCode] = (int)response.StatusCode;
            _scenarioContext[ContextConstants.ContentType] = response.ContentType;
            _scenarioContext[ContextConstants.OwnersResponse] = response.Data;
        }

        [Then("the retrieved pet matches the created pet data")]
        public void ThenTheRetrievedPetMatchesTheCreatedPetData()
        {
            var createdPet = _scenarioContext.Get<PetDto>(ContextConstants.CreatedPet);
            var retrievedPet = _scenarioContext.Get<PetDto>(ContextConstants.OwnersResponse);

            using (new AssertionScope())
            {
                retrievedPet.Should().NotBeNull("Response must contain pet data");
                retrievedPet.Should().BeEquivalentTo(createdPet);
            }
        }
    }
}
