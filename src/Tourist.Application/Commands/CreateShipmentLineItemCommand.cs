using System.Text.Json.Serialization;
using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;

public class CreateShipmentLineItemCommand : ICommand
{
    [JsonPropertyName("Barcode")]
    public string Barcode { get; set; }

    public Guid CustomerId { get; set; }
}

public class CreateShipmentLineItemCommandHandler : ICommandHandler<CreateShipmentLineItemCommand>
{
    readonly ICustomerRepository _customerRepository;

    public CreateShipmentLineItemCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
        
    public async Task HandleAsync(CreateShipmentLineItemCommand command)
    {
        var qty = BarcodeService.GetWeight(command.Barcode);
        var customer = _customerRepository.SelectById(command.CustomerId);
        //var existingShipment = get shipment items from repo
    }
}