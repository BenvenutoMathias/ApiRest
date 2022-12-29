using RestApi.DataAccess;
using RestApi.Entities.DataEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public interface IGeneralServices
    {
        public Task<User> FindUserByEmail(string email);
        public Task<List<Student>> GetAdultsStudents();
        public Task<List<Student>> GetStudentsWithMoreThanOneCourse();
        public Task<List<Course>> GetCoursesByLevelWithMoreThanOneStudent(Level level);
        public Task<List<Course>> GetCoursesByCategorie(Level level, string category);
        public Task<List<Course>> GetCoursesEmptys();
        public Task<List<User>> GetUsers();
    }
}
