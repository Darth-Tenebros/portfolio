using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using portfolio.Models;

namespace portfolio.Interfaces
{
    public interface IImageRepository
    {
        bool CreateImage(Image image);
        bool DeleteImage(Image image);
        bool Save();

    }
}