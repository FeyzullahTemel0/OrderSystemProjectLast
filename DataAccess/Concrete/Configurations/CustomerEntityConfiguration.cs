using Core.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Configurations
{
    public class CustomerEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.Property(x => x.CustomerName);
            builder.Property(x => x.CustomerCode);
            builder.Property(x => x.Address);
            builder.Property(x => x.PhoneNumber);
            builder.Property(x => x.Email);
        }
    }
}
