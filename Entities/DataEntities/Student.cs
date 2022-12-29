using Microsoft.Build.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RestApi.Entities.DataEntities
{
    public class Student : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; } = new int();

        [Required]
        public DateTime Dob { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
