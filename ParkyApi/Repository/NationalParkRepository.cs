using System.Collections.Generic;
using System.Linq;
using Parky.Data;
using Parky.Entity;
using Parky.Repository.IRepository;

namespace Parky.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;

        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalParks.OrderBy(a => a.Name).ToList();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _context.NationalParks.FirstOrDefault(n => n.Id == nationalParkId);
        }

        public bool NationalParkExists(string name)
        {
            bool isExisted = _context.NationalParks.Any(n =>
                n.Name.ToLower().Trim() == name.ToLower().Trim());
            return isExisted;
        }

        public bool NationalParkExists(int id)
        {
            bool isExisted = _context.NationalParks.Any(n => n.Id == id);
            return isExisted;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}