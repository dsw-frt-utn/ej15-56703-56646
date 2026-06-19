using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        IEnumerable<Speciality> GetAllSpecialities();
        IEnumerable<Doctor> GetAllDoctors();
        void AddDoctor(Doctor doctor);
        Speciality? GetSpecialityById(Guid id);
        Doctor? GetDoctorById(Guid id);
    }
}
