using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfolio.Dto
{
    public class ProjectDto
    {
        public string? name {get; set;}
        public string? html_url {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}
        public string? language {get;set;}
    }
}