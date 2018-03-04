using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courier.Core.Domain;
using Courier.Core.Dto;
using Courier.Core.Queries;

namespace Courier.Core.Services
{
    public class ParcelService : IParcelService
    {
        private static readonly ISet<Parcel> _parcels = new HashSet<Parcel>();
        private readonly ILocationService _locationService;

        public ParcelService(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<ParcelDetailsDto> GetAsync(Guid id)
        {
            await Task.CompletedTask;
            var parcel = _parcels.SingleOrDefault(x => x.Id == id);

            return parcel == null ? null : new ParcelDetailsDto
            {
                Id = parcel.Id,
                Name = parcel.Name,
                SentAt = parcel.SentAt,
                Received = parcel.ReceivedAt.HasValue,
                SenderId = parcel.SenderId,
                ReceiverId = parcel.ReceiverId,
                ReceivedAt = parcel.ReceivedAt
            };
        }

        public async Task<PagedResult<ParcelDto>> BrowseAsync(BrowseParcels query)
        {
            await Task.CompletedTask;
            
            return _parcels.Select(x => new ParcelDto
            {
                Id = x.Id,
                Name = x.Name,
                SentAt = x.SentAt,
                Received = x.ReceivedAt.HasValue,                
            })
            .Paginate(query);
        }

        public async Task CreateAsync(Guid id, string name, Guid senderId, 
            Guid receiverId, string receiverAddress)
        {
            var address = await _locationService.GetAsync(receiverAddress);
            if (address == null)
            {
                throw new ArgumentException($"Invalid receiver address: '{receiverAddress}'.", 
                    nameof(receiverAddress));
            }
            var sender = GetUser(senderId);
            var receiver = GetUser(receiverId);
            var parcel = new Parcel(id, name, sender, receiver, null, 
                Address.Create(address.Latitude, address.Longitude, address.Location));
            _parcels.Add(parcel);
        }

        public async Task<bool> DeliveryAvailableAsync(string address)
            => await _locationService.ExistsAsync(address);

        private User GetUser(Guid id)
            => new User(id, $"{id}@email.com", "test", "test");
    }
}