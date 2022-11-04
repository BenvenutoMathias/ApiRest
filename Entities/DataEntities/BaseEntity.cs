using System;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Entities.DataEntities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        //string empty para que no tire warning de que no puede contener null
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
