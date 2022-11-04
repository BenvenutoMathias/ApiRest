using RestApi.Entities.DataEntities;
using System.Collections.Generic;

namespace RestApi.Services
{
    // Lo ideal tener servicios para cada uno de los modelos
    public class StudentsService : IStudentsService
    {
        public IEnumerable<Student> GetStudentsWithCourses()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            throw new System.NotImplementedException();
        }
    }
}
