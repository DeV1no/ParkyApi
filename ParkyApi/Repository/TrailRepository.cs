using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Parky.Data;
using Parky.Entity;
using Parky.Repository.IRepository;

namespace Parky.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _context;

        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Trails> GetTrails()
        {
            return _context.Trails.
                Include(n => n.NationalPark)
                .OrderBy(t => t.name).ToList();
        }

        public ICollection<Trails> GetTrailsInNationalPark(int npId)
        {
            return _context.Trails.Include(n => n.NationalPark)
                .Where(t => t.NationalParkId == npId).ToList();
        }

        public Trails GetTrail(int trailId)
        {
            return _context.Trails
                .Include(n => n.NationalPark)
                .FirstOrDefault(t => t.Id == trailId);
        }

        public bool TrailExisted(string name)
        {
            bool isExisted = _context.Trails.Any(t
                => t.name.Trim().ToLower() == name.Trim().ToLower());
            return isExisted;
        }

        public bool TrailExisted(int id)
        {
            bool isExisted = _context.Trails.Any(t => t.Id == id);
            return isExisted;
        }

        public bool CreateTrail(Trails trails)
        {
            _context.Trails.Add(trails);
            return Save();
        }

        public bool UpdateTrail(Trails trails)
        {
            _context.Trails.Update(trails);
            return Save();
        }

        public bool DeleteTrail(Trails trails)
        {
            _context.Trails.Remove(trails);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}