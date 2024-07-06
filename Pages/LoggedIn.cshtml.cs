using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace portfolio.Pages
{
    public class LoggedIn : PageModel
    {
        private readonly ILogger<LoggedIn> _logger;

        public string UserName {get; set; }

        public LoggedIn(ILogger<LoggedIn> logger)
        {
            _logger = logger;
        }

        public void OnGet(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("username is null or empty");
            }

            UserName = username;
        }
    }
}