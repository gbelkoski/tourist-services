using System.Text.Json.Serialization;

namespace Tourist.Application.Commands;

public class CreateCustomerCommand : ICommand
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Address")]
    public string Address { get; set; }
}