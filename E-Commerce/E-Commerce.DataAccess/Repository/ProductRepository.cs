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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public void Update(Product product)
        {
            _context.products.Update(product);
        }
        public List<int> Select(Expression<Func<Product, int>> filter)
        {
            return _context.Set<Product>().Select(filter).ToList();
        }

        public List<string> Select(Expression<Func<Product, string>> filter)
        {
            return _context.Set<Product>().Select(filter).ToList();
        }

    }
}
