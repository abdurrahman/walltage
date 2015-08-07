using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class ResolutionRepo : IResolutionRepository
    {
        private static ResolutionRepo _instance;
        public static ResolutionRepo Instance
        {
            get { return _instance ?? (_instance = new ResolutionRepo()); }
        }

        public bool Insert(Resolution entity)
        {
            using (var db = new WalltageDbContext())
            {
                db.Resolutions.Add(entity);
                return db.SaveChanges() > 0 ? true : false;
            }
        }

        public bool Update(Resolution entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Resolution entity)
        {
            throw new NotImplementedException();
        }

        public Resolution FindById(int id)
        {
            using (var db = new WalltageDbContext())
            {
                return db.Resolutions.Find(id);
            }
        }


        public IEnumerable<Resolution> GetAll()
        {
            using (var db = new WalltageDbContext())
            {
                return db.Resolutions.ToList();
            }
        }
    }
}
