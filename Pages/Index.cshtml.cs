using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using portfolio.Github;
using portfolio.Models;

namespace portfolio.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public ICollection<Project> Repos { get; set; } = new List<Project>();
    public string? Name {get; set;}

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        
    }

    public void OnGet()
    {
        Repos = new GithubService().FetchAndDisplayRepos("darth-tenebros");
    }
}
