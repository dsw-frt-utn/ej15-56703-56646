using Dsw2026Ej15.Domain;
using System.Linq;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEf(AppDbContext context)
        {
            _context = context;
        }

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public List<Doctor> GetActiveDoctors()
        {
            return _context.Doctors.Where(d => d.IsActive).ToList();
        }

        public Doctor GetDoctorById(Guid id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public Speciality GetSpecialityById(Guid id)
        {
            return _context.Specialities.Find(id);
        }

        public IEnumerable<Speciality> GetAllSpecialities()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            throw new NotImplementedException();
        }
    }
}