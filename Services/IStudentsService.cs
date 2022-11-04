using RestApi.Entities.DataEntities;
using System.Collections.Generic;

namespace RestApi.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
    }
}
