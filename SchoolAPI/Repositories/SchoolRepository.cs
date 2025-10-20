using SchoolAPI.Models;

namespace SchoolAPI.Repositories
{
    public class SchoolRepository : IUniversityRepository
    {
        private readonly SchoolDbContext _context;
        public SchoolRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public IEnumerable<School> GetSchools()
        {
            return _context.Schools.ToList();
        }

        public School GetSchoolById(int id)
        {
            return _context.Schools.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<School> GetSchoolsByName(string name)
        {
            return _context.Schools.Where(s => s.Name.Contains(name)).ToList();
        }

        public void AddSchool(School school)
        {
            _context.Schools.Add(school);
            _context.SaveChanges(); 
        }

        public void UpdateSchool(School school)
        {
            _context.Schools.Update(school);
            _context.SaveChanges();
        }

        public void DeleteSchool(int id)
        {
            var schoolToDelete = _context.Schools.FirstOrDefault(s => s.Id == id);
            if (schoolToDelete != null)
            {
                _context.Schools.Remove(schoolToDelete);
                _context.SaveChanges();
            }
        }
    }
}
