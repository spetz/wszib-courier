using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courier.Core.Domain;

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
            => new User($"{id}@email.com", "test", "test");
    }
}