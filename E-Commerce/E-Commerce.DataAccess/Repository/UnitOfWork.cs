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
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _context;
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }


        public UnitOfWork(ApplicationDbContext context)
        {

            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
        }
       

        public void Save()
        {
            _context.SaveChanges();
        }

        

    }
}
