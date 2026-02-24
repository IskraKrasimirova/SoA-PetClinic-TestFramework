using PetClinic.ApiTests.Apis;
using PetClinic.ApiTests.Models.Dtos;
using PetClinic.ApiTests.Utils;
using Reqnroll;

namespace SeleniumFramework.ApiTests.Hooks
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

        [AfterScenario("CreatePet", Order = 1)]
        public void CleanupPet()
        {
            var createdPet = _scenarioContext.Get<PetDto>(ContextConstants.CreatedPet);
            _petsApi.DeletePet(createdPet.Id);
        }

        [AfterScenario("CreateOwner", Order = 2)]
        public void CleanupOwner()
        {
            var createdOwner = _scenarioContext.Get<OwnerDto>(ContextConstants.CreatedOwner);
            _ownersApi.DeleteOwner(createdOwner.Id);
        }
    }
}
