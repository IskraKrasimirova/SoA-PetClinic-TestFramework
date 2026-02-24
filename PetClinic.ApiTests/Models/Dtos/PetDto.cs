using System.Text.Json.Serialization;

namespace PetClinic.ApiTests.Models.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("birthDate")]
        public string BirthDate { get; set; }
        public PetTypeDto Type { get; set; }

        [JsonPropertyName("ownerId")]
        public int OwnerId { get; set; }

        public IList<VisitDto> Visits { get; set; } = new List<VisitDto>();
    }
}
