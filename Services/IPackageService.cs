using RoskhTest.Models;

namespace RoskhTest.Services
{
    public interface IPackageService
    {
        string GeneratePackageCode(string pattern);
        DeliveryState GenerateDeliveryState();
    }
}