using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new();
        private readonly List<Speciality> _specialities = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var loadedSpecialities = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                if (loadedSpecialities != null)
                {
                    _specialities.AddRange(loadedSpecialities);
                }
            }
        }

        public IEnumerable<Speciality> GetAllSpecialities() => _specialities;
        public IEnumerable<Doctor> GetAllDoctors() => _doctors;
        public void AddDoctor(Doctor doctor) => _doctors.Add(doctor);
        public Speciality? GetSpecialityById(Guid id) => _specialities.FirstOrDefault(s => s.Id == id);
        public Doctor? GetDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id);


    }
}
