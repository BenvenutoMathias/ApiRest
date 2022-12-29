using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi.Entities.DataEntities
{
    public enum Roles
    {
        User,
        Administrator
    }

    public class User : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string EmailAdress { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public int Role { get; set; } = Roles.User.GetHashCode();
    }
}