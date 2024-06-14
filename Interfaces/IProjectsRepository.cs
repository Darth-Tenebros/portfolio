using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using portfolio.Models;

namespace portfolio.Interfaces
{
    public interface IProjectsRepository
    {
        bool ProjectsExists(string name);
        ICollection<Project> GetProjects();
        bool UpdateProject(Project project);
        bool CreateProject(Project project);
        bool Save();
    }
}