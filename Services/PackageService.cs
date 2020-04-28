using System;
using Fare;
using RoskhTest.Models;

namespace RoskhTest.Services
{
    public class PackageService : IPackageService
    {
        public string GeneratePackageCode(string pattern)
        {
            var generator = new Xeger(pattern);
            return generator.Generate();
        }

        public DeliveryState GenerateDeliveryState()
        {
            var states = Enum.GetValues(typeof(DeliveryState));
            return (DeliveryState)states.GetValue(new Random().Next(states.Length));
        }
    }
}