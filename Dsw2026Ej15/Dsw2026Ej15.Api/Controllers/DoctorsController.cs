using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Name es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("LicenseNumber es requerido.");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
                throw new ValidationException("SpecialityId no existe.");

            var newDoctor = new Doctor
            {
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                Speciality = speciality,
                IsActive = true 
            };

            _persistence.AddDoctor(newDoctor);

            return StatusCode(201); 
        }

        // Endpoint 2: GET api/doctors
        [HttpGet]
        public IActionResult GetActiveDoctors()
        {
            var activeDoctors = _persistence.GetAllDoctors().Where(d => d.IsActive).ToList();
            return Ok(activeDoctors); 
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound(); 

            var response = new DoctorResponseDto
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name ?? "Sin especialidad"
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound(); 

            doctor.IsActive = false;

            return NoContent();
        }
    }

    public class CreateDoctorDto
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public Guid SpecialityId { get; set; }
    }

    public class DoctorResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string SpecialityName { get; set; } = string.Empty;
    }
}