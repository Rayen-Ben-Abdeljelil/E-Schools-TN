using SchoolAPI.Models;

namespace SchoolAPI.Repositories
{
        public interface IUniversityRepository
            {
                IEnumerable<School> GetSchools();
                School GetSchoolById(int id);
                IEnumerable<School> GetSchoolsByName(string name);
                void AddSchool(School school);
                void UpdateSchool(School school);
                void DeleteSchool(int id);
            }
    
}
