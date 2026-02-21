using FluentAssertions;
using Reqnroll;
using SeleniumFramework.ApiTests.Apis;
using SeleniumFramework.ApiTests.Models.Dtos;
using SeleniumFramework.ApiTests.Utils;

namespace SeleniumFramework.ApiTests.Steps
{
    [Binding]
    public class PetTypesApiSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PetTypesApi _petTypesApi;

        public PetTypesApiSteps(ScenarioContext scenarioContext, PetTypesApi petTypesApi)
        {
            this._scenarioContext = scenarioContext;
            this._petTypesApi = petTypesApi;
        }

        [Given("I make a get request to pet types endpoint")]
        public void GivenIMakeAGetRequestToPetTypesEndpoint()
        {
            var response = _petTypesApi.GetPetTypes();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Should().NotBeNull();

            _scenarioContext.Add(ContextConstants.PetTypesResponse, response.Data);
        }

        [Given("I select an existing pet type from the response")]
        public void GivenISelectAnExistingPetTypeFromTheResponse()
        {
            var petTypesList = _scenarioContext.Get<IReadOnlyCollection<PetTypeDto>>(ContextConstants.PetTypesResponse);
            petTypesList.Should().NotBeNullOrEmpty();

            var random = new Random();
            var selectedPetType = petTypesList.ElementAt(random.Next(petTypesList.Count));
            selectedPetType.Should().NotBeNull();

            _scenarioContext.Add(ContextConstants.SelectedPetType, selectedPetType);
        }
    }
}
