using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Courier.Core.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Courier.Core.Services
{
    public class LocationService : ILocationService
    {
        private static readonly IDictionary<string, AddressDto> _addresses = new Dictionary<string, AddressDto>();
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json")
        };

        public async Task<bool> ExistsAsync(string address)
            => await GetAsync(address) != null;

        public async Task<AddressDto> GetAsync(string address)
        {
            var key = address.ToLowerInvariant();
            if (_addresses.ContainsKey(key))
            {
                return _addresses[key];
            }
            var response = await _client.GetAsync($"?address={address}&key=AIzaSyD5UaNtOrvxjvxUJscB1qsCfHrPWv6UTtk");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationResponse>(content);
            var result = location.Results?.FirstOrDefault();
            var dto = new AddressDto
                {
                    Latitude = result.Geometry.Location.Lat,
                    Longitude = result.Geometry.Location.Lng,
                    Location = result.FormattedAddress
                };
            _addresses[key] = dto;

            return dto;
        }

        private class LocationResponse
        {
            public IEnumerable<LocationResult> Results { get; set; }
        }

        private class LocationResult
        {
            [JsonProperty(PropertyName = "formatted_address")]
            public string FormattedAddress { get; set; }

            [JsonProperty(PropertyName = "address_components")]
            public IEnumerable<AddressComponent> AddressComponents { get; set; }
            
            public Geometry Geometry { get; set; }
        }

        private class AddressComponent
        {
            [JsonProperty(PropertyName = "long_name")]
            public string LongName { get; set; }

            [JsonProperty(PropertyName = "short_name")]
            public string ShortName { get; set; }

            public IEnumerable<string> Types { get; set; }
        }

        private class Geometry
        {
            public GeometryLocation Location { get; set; }
        }

        private class GeometryLocation
        {
            public double Lat { get; set; }
            public double Lng { get; set; } 
        }
    }
}