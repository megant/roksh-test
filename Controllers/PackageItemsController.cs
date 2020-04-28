using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoskhTest.Data;
using RoskhTest.ViewModels;

namespace RoskhTest.Controllers
{
    [Authorize]
    public class PackageItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PackageItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult GetItems(string id)
        {
            IList<ViewItem> packageItems = null;
            try
            {
                packageItems = _context.PackageItems
                    .Include(x => x.Package)
                    .Include(x => x.Item)
                    .Where(x => x.Package.Code == id)
                    .Select(pi => pi.Item.ToViewItem()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            if (!packageItems.Any())
            {
                return NotFound("No items found");
            }

            return Ok(packageItems);
        }
    }
}

