using RestSharp;
using SeleniumFramework.ApiTests.Models.Dtos;
using SeleniumFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.ApiTests.Apis
{
    public class PetsApi
    {
        private readonly RestClient _client;
        private readonly string _uri;

        public PetsApi(RestClient restClient)
        {
            _client = restClient;
            _uri = "petclinic/api/pets";
        }

        public RestResponse<PetDto> DeletePet(int petId)
        {
            var request = new RestRequest($"{_uri}/{petId}", Method.Delete);
            return _client.Execute<PetDto>(request);
        }
    }
}
