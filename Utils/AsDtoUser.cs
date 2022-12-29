using RestApi.Dtos;
using RestApi.Entities.DataEntities;

namespace RestApi.Utils
{
    public static class AsDtoUser
    {
        public static UserDto AsDto(this User user)
        {
            var userDto = new UserDto
            {
                Name = user.Name,
                LastName = user.LastName
            };

            return userDto;
        }
    }
}
