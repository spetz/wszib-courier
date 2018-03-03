using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courier.Core.Dto;
using Courier.Core.Queries;

namespace Courier.Core.Services
{
    public interface IParcelService
    {
        Task<ParcelDetailsDto> GetAsync(Guid id);
        Task<PagedResult<ParcelDto>> BrowseAsync(BrowseParcels query);
        Task CreateAsync(Guid id, string name, Guid senderId, 
            Guid receiverId, string receiverAddress);
        Task<bool> DeliveryAvailableAsync(string address);
    }
}