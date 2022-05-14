using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineShop.Data.Entities
{
    public class BaseEntity:IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }
    }
}
