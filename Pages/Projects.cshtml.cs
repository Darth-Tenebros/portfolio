using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    private readonly IProjectsRepository _projectRepossitory;
    private readonly IImageRepository _imageRepository;
    public ICollection<Project> projects {get; set;}
    public ICollection<Image> images {get; set;}

    public PrivacyModel(ILogger<PrivacyModel> logger, IProjectsRepository projectsRepository, IImageRepository imageRepository)
    {
        _logger = logger;
        _projectRepossitory = projectsRepository;
        _imageRepository = imageRepository;
    }

    public void OnGet()
    {
        projects = _projectRepossitory.GetProjects();
        images = _imageRepository.GetImages();
    }
}

