
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
	public class WareHouseRepository : EfEntityRepositoryBase<WareHouse, ProjectDbContext>, IWareHouseRepository
	{
		// uyarı verdiğinde implement et
		public WareHouseRepository(ProjectDbContext context) : base(context)
		{
		}

		public async Task<bool> ExistWareHouseByProductIdAndAmount(int productId, int amount,int Size)
		{
			// Sorgumuzu yazdık.
			return await Context.WareHouses.AnyAsync(x => x.ProductId == productId && x.Amount >= amount && x.Size== Size && x.isDeleted == false);
		}

		

		public async Task<List<WareHouseDto>> GetAllWareHouseDetailsDto()
		{
			var result = await (
							from warehouse in Context.WareHouses
							join product in Context.Products
							on warehouse.ProductId equals product.Id 
							where warehouse.isDeleted == false
							select new WareHouseDto()
							{
								CreatedDate = warehouse.CreatedDate,
								CreatedUserId = warehouse.CreatedUserId,
								Amount = warehouse.Amount,
								isDeleted = warehouse.isDeleted,
								isReady = warehouse.isReady,
								LastUpdatedDate = warehouse.LastUpdatedDate,
								Id = warehouse.Id,
								LastUpdatedUserId = warehouse.LastUpdatedUserId,
								ProductId = warehouse.ProductId,
								ProductName = product.Name,
								Size = warehouse.Size,
								Status = warehouse.Status,
								
							}).ToListAsync();
			return	result;
			
		}
	}
}
