using System;

namespace MyOnlineShop.Data.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        int? CreatedById { get; set; }
        int? UpdatedById { get; set; }
        bool IsActive { get; set; }
    }
}
