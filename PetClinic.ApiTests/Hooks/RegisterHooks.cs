using FluentAssertions;
using PetClinic.ApiTests.Apis;
using PetClinic.ApiTests.Models.Dtos;
using PetClinic.ApiTests.Utils;
using Reqnroll;

namespace PetClinic.ApiTests.Hooks
{
    [Binding]
    public class RegisterHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OwnersApi _ownersApi;
        private readonly PetsApi _petsApi;

        public RegisterHooks(ScenarioContext scenarioContext, OwnersApi ownersApi, PetsApi petsApi)
        {
            this._scenarioContext = scenarioContext;
            this._ownersApi = ownersApi;
            this._petsApi = petsApi;
        }

        [AfterScenario("CleanupPet", Order = 1)]
        public void CleanupPet()
        {
            var createdPet = _scenarioContext.Get<PetDto>(ContextConstants.CreatedPet);
            var response = _petsApi.DeletePet(createdPet.Id);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [AfterScenario("CleanupOwner", Order = 2)]
        public void CleanupOwner()
        {
            var createdOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.CreatedOwner);
            var response = _ownersApi.DeleteOwner(createdOwner.Id);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [AfterScenario("CreatedOwnerByFailedTest", Order = 3)]
        public void DeleteOwner()
        {
            var createdOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.RawResponse);
            var response = _ownersApi.DeleteOwner(createdOwner.Id);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
    }
}
