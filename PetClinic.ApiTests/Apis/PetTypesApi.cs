using PetClinic.ApiTests.Models.Dtos;
using RestSharp;

namespace PetClinic.ApiTests.Apis
{
    public class PetTypesApi
    {
        private readonly RestClient _client;
        private readonly string _uri;

        public PetTypesApi(RestClient restClient)
        {
            _client = restClient;
            _uri = "petclinic/api/pettypes";
        }

        public RestResponse<IReadOnlyCollection<PetTypeDto>> GetPetTypes()
        {
            var request = new RestRequest(_uri, Method.Get);
            return _client.Execute<IReadOnlyCollection<PetTypeDto>>(request);
        }
    }
}
