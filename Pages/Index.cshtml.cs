using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using portfolio.Github;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly GithubService _githubService;
    public ICollection<Project> Repos { get; set; } = new List<Project>();
    public string Name {get; set;}

    public IndexModel(ILogger<IndexModel> logger, GithubService githubService)
    {
        _logger = logger;
        _githubService = githubService;
        
    }

    public void OnGet()
    {
    }
}
