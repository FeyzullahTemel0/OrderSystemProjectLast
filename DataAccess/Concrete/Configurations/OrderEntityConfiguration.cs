using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Configurations
{
    public class OrderEntityConfiguration
    {
        public  void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.CustomerId);
            builder.Property(x => x.ProductId);
            builder.Property(x => x.Amount);
          
        }
    }
}
