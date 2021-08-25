using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            PopularTags();
            BigCollections();
            LastItems();
            return View();
        }

        public void LastItems()
        {
            var itemsQuery = from i in _context.Item
                                orderby i.Created
                                select i;

            ViewBag.LastItems = itemsQuery.Reverse().ToList();
        }

        public void BigCollections()
        {
            var collectionsQuery = from c in _context.Collection
                                        orderby c.ItemsAmount
                                        select c;

            ViewBag.BigCollections = collectionsQuery.Reverse().ToList();
        }

        public void PopularTags()
        {
            var tagsQuery = from t in _context.Tag
                            where t.Count > 0
                            orderby t.Count
                            select t;

            ViewBag.Tags = tagsQuery.Reverse().ToList();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
