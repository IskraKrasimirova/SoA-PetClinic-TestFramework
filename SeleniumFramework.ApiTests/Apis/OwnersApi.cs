using RestSharp;
using SeleniumFramework.ApiTests.Models.Dtos;
using SeleniumFramework.Models;

namespace SeleniumFramework.ApiTests.Apis;

public class OwnersApi
{
    private readonly RestClient _client;
    private readonly string _uri;

    public OwnersApi(RestClient restClient)
    {
        _client = restClient;
        _uri = "petclinic/api/owners";
    }

    public RestResponse<OwnerDto> GetUserById(int id)
    {
        var request = new RestRequest($"{_uri}/{id}", Method.Get);
        return _client.Execute<OwnerDto>(request);
    }

    public RestResponse<IReadOnlyCollection<OwnerDto>> GetOwners()
    {
        var request = new RestRequest(_uri, Method.Get);
        return _client.Execute<IReadOnlyCollection<OwnerDto>>(request);
    }

    public RestResponse<PetDto> CreatePetForOwner(int ownerId, PetDto newPet)
    {
        var request = new RestRequest($"{_uri}/{ownerId}/pets", Method.Post);
        request.AddJsonBody(newPet);
        return _client.Execute<PetDto>(request);
    }

    public RestResponse<PetDto> GetPetById(int ownerId, int petId)
    {
        var request = new RestRequest($"{_uri}/{ownerId}/pets/{petId}", Method.Get);
        return _client.Execute<PetDto>(request);
    }
}