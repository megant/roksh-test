using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RoskhTest.Models;
using WebsiteParser;

namespace RoskhTest.Services
{
    public class CollectorService : ICollectorService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CollectorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<string> GetWebsiteContent(string url)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        
        public IEnumerable<Item> CollectItems(string url)
        {
            var websiteRequest = Task.Run(() => GetWebsiteContent(url));
            var content = websiteRequest.Result;
            var items = WebContentParser.ParseList<Item>(content);
            return items;
        }
    }
}