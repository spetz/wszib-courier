using System.Threading.Tasks;
using Courier.Core.Dto;

namespace Courier.Core.Services
{
    public interface ILocationService
    {
        Task<bool> ExistsAsync(string address);
        Task<AddressDto> GetAsync(string address);
    }
}