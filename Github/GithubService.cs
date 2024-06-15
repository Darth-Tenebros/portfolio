using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Github
{
    public class GithubService
    {
        private readonly IProjectsRepository _projectsRepository;

        public GithubService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public GithubService(){}

        public List<Project> FetchAndDisplayRepos(string username)
        {
            // TODO: find a cleaner way to do this
            string url = $"https://api.github.com/users/{username}/repos";
            var repos = new List<Project>();

            using (HttpClient client = new HttpClient())
            {
                // GitHub API requires a User-Agent header
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    repos = JsonConvert.DeserializeObject<List<Project>>(responseData);

                    foreach (var project in repos)
                    {
                        project.Id = 0;
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to fetch repositories: {response.StatusCode}");
                }
            }

            foreach(Project project in repos)
            {
                bool test = _projectsRepository.ProjectsExists(project.name);
                if(test)
                {
                    _projectsRepository.UpdateProject(project);
                    continue;
                }
                else
                {
                    _projectsRepository.CreateProject(project);
                }
            }

            return repos;
        }

        static void PrintRepos(List<Project> repos)
        {
            foreach (var repo in repos)
            {
                Console.WriteLine($"Repo Name: {repo.name}");
                Console.WriteLine($"Repo URL: {repo.html_url}");
            }
        }
    }
}