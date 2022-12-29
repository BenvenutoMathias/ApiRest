using Microsoft.EntityFrameworkCore;
using RestApi.DataAccess;
using RestApi.Entities.DataEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public class GeneralServices : IGeneralServices
    {
        private readonly UniversityDBContext _context;

        public GeneralServices(UniversityDBContext context)
        { 
            _context = context;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var users = await _context.Users.ToListAsync();

            return users.SingleOrDefault(user => user.EmailAdress == email);
        }

        public async Task<List<Student>> GetAdultsStudents()
        {
            var students = await _context.Students.ToListAsync();

            return students.Where(student => student.Age >= 18).ToList();
        }

        public async Task<List<Student>> GetStudentsWithMoreThanOneCourse()
        {
            var students = await _context.Students.ToListAsync();

            return students.Where(student => student.Courses.Count > 0).ToList();
        }

        public async Task<List<Course>> GetCoursesByLevelWithMoreThanOneStudent(Level level)
        {
            var courses = await _context.Courses.ToListAsync();

            return courses.Where(course => course.Level == level && course.Students.Count > 0).ToList(); 
        }

        public async Task<List<Course>> GetCoursesByCategorie(Level level, string category)
        {
            var courses = await _context.Courses.ToListAsync();

            return courses.Where(course => course.Level == level && course.Name == category).ToList();
        }

        public async Task<List<Course>> GetCoursesEmptys()
        {
            var courses = await _context.Courses.ToListAsync();

            return courses.Where(course => course.Students.Count == 0).ToList();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
