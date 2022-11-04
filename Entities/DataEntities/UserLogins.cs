using System.ComponentModel.DataAnnotations;

namespace RestApi.Entities.DataEntities
{
    public class UserLogins
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
