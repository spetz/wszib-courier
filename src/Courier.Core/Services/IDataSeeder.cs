using System.Threading.Tasks;

namespace Courier.Core.Services
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}