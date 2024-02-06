using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public void Update(Category category)
        {
            _context.categories.Update(category);
        }
        public List<int> Select(Expression<Func<Category, int>> filter)
        {
            return _context.Set<Category>().Select(filter).ToList();
        }

        public List<string> Select(Expression<Func<Category, string>> filter)
        {
            return _context.Set<Category>().Select(filter).ToList();
        }

    }
}
