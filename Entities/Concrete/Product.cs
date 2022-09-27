using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
    }
}
