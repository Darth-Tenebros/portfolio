using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using portfolio.Data;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Respository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateImage(Image image)
        {
            _context.Add(image);
            return Save();
        }

        public bool DeleteImage(Image image)
        {
            _context.Remove(image);
            return Save();
        }

        public Image GetImage(string title)
        {
            return _context.Images.Where(i => i.title.Equals(title)).FirstOrDefault();
        }

        public ICollection<Image> GetImages()
        {
            return _context.Images.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if(saved > 0)
                return true;
            
            return false;
        }
    }
}
