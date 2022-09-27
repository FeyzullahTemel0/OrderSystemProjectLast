using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Order : BaseEntity,IEntity
    {

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Size { get; set; }
    }
}
