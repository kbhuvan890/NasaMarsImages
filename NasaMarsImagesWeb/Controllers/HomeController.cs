using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaMarsImagesWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NasaMarsImagesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static List<Folder> GetFolders()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "MarsOutputImage");
            var folders = Directory.GetDirectories(path);

            var lfolders = new List<Folder>();
            foreach (var item in folders)
            {
                //To fetch dates
                var arr = item.Split('\\');
                lfolders.Add(new Folder { Name = arr[arr.Length - 1], Path = item });
            }

            return lfolders;
        }

        [HttpGet]
        public IActionResult Index()
        { 
            ViewBag.ListofFolders = GetFolders();
            ViewBag.Images = string.Empty;

            return View();
        }

        [HttpPost]
        public IActionResult Index(Folder folder)
        {         
            string[] imageFiles = Directory.GetFiles(folder.Path);
            List<string> lImagepaths = new List<string>();
            foreach (var img in imageFiles)
            {
                lImagepaths.Add(@"~/Home" + img.Split("MarsOutputImage")[1]);
            }

            ViewBag.Images = lImagepaths.ToArray();
            ViewBag.ListofFolders = GetFolders();

            return View();
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
