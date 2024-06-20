using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Pages
{
    public class Dashboard : PageModel
    {
        private readonly ILogger<Dashboard> _logger;
        private readonly IImageRepository _imageRepository;

        public Dashboard(ILogger<Dashboard> logger, IImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File not selected");
            }

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var imageFile = new Image
                {
                    title = file.FileName,
                    image = ms.ToArray(),
                };
                _imageRepository.CreateImage(imageFile);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}