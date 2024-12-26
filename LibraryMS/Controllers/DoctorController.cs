using Microsoft.AspNetCore.Mvc;
using SampleManagers;
using SampleModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorManager _doctorManager;

        public DoctorController(DoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Get()
        {
            var doctors = await _doctorManager.GetAllAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Get(int id)
        {
            var doctor = await _doctorManager.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Doctor doctor)
        {
            await _doctorManager.AddAsync(doctor);
            return CreatedAtAction(nameof(Get), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Doctor doctor)
        {
            if (id !=  doctor.Id)
            {
                return BadRequest();
            }

            var existingDoctor = await _doctorManager.GetByIdAsync(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            await _doctorManager.UpdateAsync(doctor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var doctor = await _doctorManager.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            await _doctorManager.DeleteAsync(id);
            return NoContent();
        }
    }
}