using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoskhTest.Data;
using RoskhTest.Models;
using RoskhTest.Services;

namespace RoskhTest.Controllers
{
    [Authorize]
    public class PackagesController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public PackagesController(
            IServiceProvider serviceProvider, 
            ApplicationDbContext context, 
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        public IActionResult GetAll()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var packages = _context.Packages
                .Include(p => p.State)
                .Where(x => x.OwnerId == currentUserId);
            var viewPackages = packages.Select(x => x.ToViewPackage());
            return Ok(viewPackages);
        }
        
        public IActionResult Generate()
        {
            if (!_context.Items.Any())
            {
                return NotFound("No items found for package generation. Please get items first!");
            }
            var itemsPerPackage = new Random().Next(1, _context.Items.Count());
            var packageService = _serviceProvider.GetService<IPackageService>();
            var packageCode = packageService.GeneratePackageCode(_configuration.GetValue<string>("Packages:PackageCodePattern"));
            var description = $"Package containing {itemsPerPackage} items, created at {DateTime.Now}";
            var randomItems = _context.Items.AsEnumerable().OrderBy(x => Guid.NewGuid()).Take(itemsPerPackage);
            var packageId = Guid.NewGuid();
            var packageItems = randomItems.Select(item => new PackageItem()
            {
                Id = Guid.NewGuid(),
                PackageId = packageId,
                ItemId = item.Id
            });
            var deliveryState = packageService.GenerateDeliveryState();
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var package = new Package()
            {
                Id = packageId,
                Code = packageCode,
                StateId = (int)deliveryState,
                State = _context.PackageStates.Find((int)deliveryState),
                OwnerId = currentUserId,
                Description = description,
                Name = $"Package {DateTime.Now}",
            };
            _context.PackageItems.AddRange(packageItems);
            _context.Packages.Add(package);
            _context.SaveChanges();

            var viewPackage = package.ToViewPackage();
            
            return Ok(viewPackage);
        }
    }
}