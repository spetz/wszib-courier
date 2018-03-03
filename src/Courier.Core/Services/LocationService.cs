using System;
using System.Net.Http;
using System.Threading.Tasks;
using Courier.Core.Dto;

namespace Courier.Core.Services
{
    public class LocationService : ILocationService
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json")
        };

        public async Task<AddressDto> GetAsync(string address)
        {
            var response = await _client.GetAsync($"?address={address}&key=AIzaSyD5UaNtOrvxjvxUJscB1qsCfHrPWv6UTtk");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();

            return new AddressDto();
        }
    }
}