using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfolio.Models
{
    public class Project
    {
        public int Id {get; set;}
        public string? name {get; set;}
        public string? html_url {get; set;}
        public string? description {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}
        public string? language {get;set;}
    }
}