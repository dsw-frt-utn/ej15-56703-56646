using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
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
            return _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToList();
        }

        public Doctor GetDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id);
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
            return _context.Specialities.ToList();
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .ToList();
        }
    }
}
