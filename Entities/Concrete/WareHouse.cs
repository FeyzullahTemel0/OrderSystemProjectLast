using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class WareHouse : BaseEntity, IEntity
    {
        
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public bool isReady { get; set; }
        public int Size { get; set; }
    }
}
