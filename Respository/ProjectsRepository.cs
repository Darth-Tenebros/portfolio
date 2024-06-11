using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using portfolio.Data;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Respository
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly DataContext _context;
        public ProjectsRepository(DataContext context)
        {
            _context = context;
        }
        
        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Projects.OrderBy(p => p.Id).ToList();
        }

        public bool ProjectsExists(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if(saved > 0)
                return true;
            
            return false;
        }

        public bool UpdateProject(Project project)
        {
            _context.Update(project);
            return Save();
        }
    }
}