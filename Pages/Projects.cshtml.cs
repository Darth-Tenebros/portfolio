using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    private IProjectsRepository _projectRepossitory;
    public ICollection<Project> projects {get; set;}

    public PrivacyModel(ILogger<PrivacyModel> logger, IProjectsRepository projectsRepository)
    {
        _logger = logger;
        _projectRepossitory = projectsRepository;
    }

    public void OnGet()
    {
        projects = _projectRepossitory.GetProjects();
    }
}

