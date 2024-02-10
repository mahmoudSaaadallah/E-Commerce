using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        List<int> Select(Expression<Func<Product, int>> filter);
        List<string> Select(Expression<Func<Product, string>> filter);

    }
}
