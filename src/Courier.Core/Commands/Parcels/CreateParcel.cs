using System;
using Newtonsoft.Json;

namespace Courier.Core.Commands.Parcels
{
    public class CreateParcel : ICommand
    {
        public Guid UserId { get; set; }
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public Guid ReceiverId { get; }
        public string ReceiverAddress { get; }
        
        [JsonConstructor]
        public CreateParcel(string name, Guid receiverId, string receiverAddress)
        {
            Name = name;
            ReceiverId = receiverId;
            ReceiverAddress = receiverAddress;
        }
    }
}