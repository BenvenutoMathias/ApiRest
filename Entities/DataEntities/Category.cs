using Microsoft.Build.Framework;
using System.Collections;
using System.Collections.Generic;

namespace RestApi.Entities.DataEntities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
