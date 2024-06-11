using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Build.Evaluation;
using portfolio.Dto;

namespace portfolio.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}