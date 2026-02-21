using SeleniumFramework.Models;
using System.Text.Json.Serialization;

namespace SeleniumFramework.ApiTests.Models.Dtos;

public class OwnerDto
{
    public int Id { get; set; }

    [JsonPropertyName("firstName")] 
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    public string City { get; set; }

    public string Telephone { get; set; }

    public IList<PetDto> Pets { get; set; } = new List<PetDto>();
}