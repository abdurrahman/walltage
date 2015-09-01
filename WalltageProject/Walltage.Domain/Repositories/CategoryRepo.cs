using System;
using System.Collections.Generic;
using System.Linq;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class CategoryRepo : ICategoryRepository, IDisposable
    {
        private static CategoryRepo _instance;
        public static CategoryRepo Instance
        {
            get { return _instance ?? (_instance = new CategoryRepo()); }
        }

        public bool Insert(Category entity)
        {
            using (var db = new WalltageDbContext())
            {
                db.Categories.Add(entity);
                return db.SaveChanges() > 0 ? true : false;
            }
        }

        public bool Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category FindById(int id)
        {
            using (var db = new WalltageDbContext())
            {
                return db.Categories.Find(id);
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using (var db = new WalltageDbContext())
            {
                return db.Categories.ToList();
            }
        }

        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
