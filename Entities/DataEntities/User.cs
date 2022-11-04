using System.ComponentModel.DataAnnotations;

namespace RestApi.Entities.DataEntities
{
    public enum Role
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
        public Role Role { get; set; } = Role.User;

    }
}