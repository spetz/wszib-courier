using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courier.Core.Dto;

namespace Courier.Core.Services
{
    public interface IParcelService
    {
        Task<ParcelDetailsDto> GetAsync(Guid id);
        Task<IEnumerable<ParcelDto>> BrowseAsync();
        Task CreateAsync(Guid id, string name, Guid senderId, 
            Guid receiverId, string receiverAddress);
        Task<bool> DeliveryAvailableAsync(string address);
    }
}