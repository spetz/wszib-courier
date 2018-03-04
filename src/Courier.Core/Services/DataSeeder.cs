using System;
using System.Threading.Tasks;
using Courier.Core.Options;
using Microsoft.Extensions.Options;

namespace Courier.Core.Services
{
    public class DataSeeder : IDataSeeder
    {
        private readonly IParcelService _parcelService;
        private readonly bool _seed;

        public DataSeeder(IParcelService parcelService, IOptions<AppOptions> options)
        {
            _parcelService = parcelService;
            _seed = options.Value.SeedData;
        }

        public async Task SeedAsync()
        {   
            if(!_seed)
            {
                return;
            }
            for (int i=1; i<=10; i++)
            {
                await _parcelService.CreateAsync(Guid.NewGuid(), $"Parcel #{i}",
                    Guid.Empty, Guid.Empty, "Czarnowiejska 1");
            }
        }
    }
}