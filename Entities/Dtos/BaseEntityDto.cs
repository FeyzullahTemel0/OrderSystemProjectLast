using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BaseEntityDto : IDto
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
    }
}
