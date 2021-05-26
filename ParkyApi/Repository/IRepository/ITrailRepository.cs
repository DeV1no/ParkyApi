using System.Collections.Generic;
using Parky.Entity;

namespace Parky.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trails> GetTrails();
        ICollection<Trails> GetTrailsInNationalPark(int npId);
        Trails GetTrail(int trailId);
        bool TrailExisted(string name);
        bool TrailExisted(int id);
        bool CreateTrail(Trails trails);
        bool UpdateTrail(Trails trails);
        bool DeleteTrail(Trails trails);
        bool Save();
    }
}