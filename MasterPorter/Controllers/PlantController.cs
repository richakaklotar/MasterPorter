using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly MasterPorterContext _context;

        public PlantController(MasterPorterContext context)
        {
            _context = context;
        }

        // GET: api/Plant
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = QPrimaryService.GetExistingPlantList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    innerInnerException = ex.InnerException?.InnerException?.Message
                });
            }
        }

        // GET: api/Plant/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = QPrimaryService.GetPlant(id);

                if (data == null)
                    return NotFound(new { message = "Plant not found" });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    innerInnerException = ex.InnerException?.InnerException?.Message
                });
            }
        }

        // POST: api/Plant
        [HttpPost]
        [HttpPost]
        public IActionResult Create([FromBody] Plant plant)
        {
            try
            {
                if (plant == null)
                {
                    return BadRequest(new
                    {
                        message = "Plant data is required"
                    });
                }

                var dataModify = new DataModify();

                int result = dataModify.SaveBusiness(plant);

                if (result > 0)
                {
                    return Ok(new
                    {
                        message = "Plant saved successfully",
                        plantId = result
                    });
                }

                return BadRequest(new
                {
                    message = "Plant could not be saved"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    innerInnerException = ex.InnerException?.InnerException?.Message
                });
            }
        }

        // PUT: api/Plant/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Plant plant)
        {
            try
            {
                if (plant == null)
                {
                    return BadRequest(new
                    {
                        message = "Plant data is required."
                    });
                }

                var existingPlant = await _context.Plants
                    .FirstOrDefaultAsync(x => x.PlantId == id);

                if (existingPlant == null)
                {
                    return NotFound(new
                    {
                        message = $"Plant with ID {id} not found."
                    });
                }

                // Keep primary key unchanged
                existingPlant.PlantName = plant.PlantName;
                existingPlant.PlantCode = plant.PlantCode;
                existingPlant.Isactive = plant.Isactive;

                await _context.SaveChangesAsync();

                return Ok(existingPlant);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    message = "Unable to update plant.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while updating plant.",
                    error = ex.Message
                });
            }
        }

        // DELETE: api/Plant/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var plant = await _context.Plants
                    .FirstOrDefaultAsync(x => x.PlantId == id);

                if (plant == null)
                {
                    return NotFound(new
                    {
                        message = $"Plant with ID {id} not found."
                    });
                }

                _context.Plants.Remove(plant);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Plant deleted successfully.",
                    plantId = id
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    message = "Plant cannot be deleted because it may be used by another record.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while deleting plant.",
                    error = ex.Message
                });
            }
        }
    }
}