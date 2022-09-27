using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class BaseEntity : IEntity
    {
        public virtual int Id { get; set; }
       
    }
}
