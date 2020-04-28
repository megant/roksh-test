using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoskhTest.Data;
using RoskhTest.Services;

namespace RoskhTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ItemsController(
            IServiceProvider serviceProvider, 
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _configuration = configuration;
        }
        
        [HttpGet("feed")]
        public IActionResult Feed()
        {
            string message;
            
            if (_context.Items.Any())
            {
                message = "Items fed previously. No action needed.";
            }
            else
            {
                var itemSourceUrl = _configuration.GetValue<string>("Items:ItemSourceUrl");
                
                try
                {
                    var collectorService = _serviceProvider.GetService<ICollectorService>();
                    var items = collectorService.CollectItems(itemSourceUrl);
                    if (items.Count() < 1)
                    {
                        message = $"No valid items found requesting {itemSourceUrl}";
                    }
                    else
                    {
                        items.ToList().ForEach(item =>
                        {
                            item.Id = Guid.NewGuid();
                        });
                        _context.Items.AddRange(items);
                        _context.SaveChanges();
                        message = "Items fed successfully.";
                    }
                }
                catch (Exception ex)
                {
                    message = $"An error occured during requesting {itemSourceUrl}. Message is: {ex.Message}";
                }
            }
            return Ok(new ControllerResponse()
            {
                Message = message
            });
        }
    }
}