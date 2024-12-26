using Microsoft.AspNetCore.Mvc;
using SampleManagers;
using SampleModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly AdminManager _adminManager;

        public AdminssController(AdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> Get()
        {
            var admins = await _adminsManager.GetAllAsync();
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(int id)
        {
            var admin = await _adminManager.GetByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Admin admin)
        {
            await _adminManager.AddAsync(admin);
            return CreatedAtAction(nameof(Get), new { id = admin.Id }, admin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Admin admin)
        {
            if (id != admin.Id)
            {
                return BadRequest();
            }

            var existingBook = await _adminManager.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            await _adminManager.UpdateAsync(admin);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var admin = await _adminManager.GetByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            await _adminManager.DeleteAsync(id);
            return NoContent();
        }
    }
}