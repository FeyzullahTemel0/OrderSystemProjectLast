
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
	public class OrderRepository : EfEntityRepositoryBase<Order, ProjectDbContext>, IOrderRepository
	{
		public OrderRepository(ProjectDbContext context) : base(context)
		{

		}

		public async Task<IEnumerable<OrderDto>> GetAllOrderDto()
		{


            //Temel Linq sorgusu
            var resultData = await (from order in Context.Orders
                                    join product in Context.Products on order.ProductId equals product.Id
                                    join customer in Context.Customers on order.CustomerId equals customer.Id
                                    where order.isDeleted == false
                                    select new OrderDto()
                                    {
                                        Id = order.Id,
                                        CreatedDate = order.CreatedDate,
                                        CreatedUserId = order.CreatedUserId,
                                        LastUpdatedUserId = order.LastUpdatedUserId,
                                        LastUpdatedDate = order.LastUpdatedDate,
                                        Status = order.Status,
                                        isDeleted = order.isDeleted,
                                        ProductId = order.ProductId,
                                        CustomerId = order.CustomerId,
                                        ProductName = product.Name,
                                        CustomerName = customer.CustomerName,
                                        Amount = order.Amount,
                                        Size = order.Size

                                    }).ToListAsync();

            return resultData;

        }
	}
}
