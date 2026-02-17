
using RestSharp;
using SeleniumFramework.ApiTests.Models.Dtos;

namespace SeleniumFramework.ApiTests.Apis;

// Use this as an example. Delete before submitting the exam.
public class UsersApi
{
    private readonly RestClient _client;
    private readonly string _uri;

    public UsersApi(RestClient restClient)
    {
        _client = restClient;
        _uri = "/users";
    }

    public RestResponse<UserDto> GetUserById(int id)
    {
        var request = new RestRequest($"{_uri}/{id}", Method.Get);
        return _client.Execute<UserDto>(request);
    }
}