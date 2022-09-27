using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Configurations
{
    public class WareHouseEntityConfiguration
    {

        public  void Configure(EntityTypeBuilder<WareHouse> builder)
        {
            builder.Property(x => x.ProductId);
            builder.Property(x => x.Amount);
            builder.Property(x => x.isReady);
            builder.Property(x => x.Size);
            builder.Property(x => x.Status);

        }
    }
}
