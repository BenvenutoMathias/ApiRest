using RestApi.Entities.DataEntities;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; } = string.Empty;

        public static implicit operator UserDto(User d)
        {
            var userDto = new UserDto();
            userDto.Name = d.Name;
            userDto.LastName = d.LastName;
            userDto.EmailAdress = d.EmailAdress;
            return userDto;
        }

    }
}
