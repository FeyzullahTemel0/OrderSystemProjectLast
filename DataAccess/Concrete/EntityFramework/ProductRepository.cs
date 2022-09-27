
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : EfEntityRepositoryBase<Product, ProjectDbContext>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetProductLookUpWithCode()
        {
            var lookUp = await ( 
                                from product in Context.Products
                               select new SelectionItem()
                               {
                                   Id = product.Id.ToString(),
                                   Label = product.Name
                               }).ToListAsync();
            return lookUp;
        }
    }
}
