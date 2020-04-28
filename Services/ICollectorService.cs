using System.Collections.Generic;
using RoskhTest.Models;

namespace RoskhTest.Services
{
    public interface ICollectorService
    {
        IEnumerable<Item> CollectItems(string url);
    }
}