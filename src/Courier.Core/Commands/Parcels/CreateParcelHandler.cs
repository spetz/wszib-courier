using System.Threading.Tasks;
using Courier.Core.Services;

namespace Courier.Core.Commands.Parcels
{
    public class CreateParcelHandler : ICommandHandler<CreateParcel>
    {
        private readonly IParcelService _parcelService;

        public CreateParcelHandler(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        public async Task HandleAsync(CreateParcel command)
        {
            await _parcelService.CreateAsync(command.Id, command.Name,
                command.UserId, command.ReceiverId, command.ReceiverAddress);            
        }
    }
}