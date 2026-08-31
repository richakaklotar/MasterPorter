//using AutoEntity.EntityModels;
//using Microsoft.AspNetCore.Mvc;
//using ModifyService;
//using ReadService;

//namespace MasterPorter.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PlantController : ControllerBase
//    {
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            try
//            {
//                return Ok(QPrimaryService.GetExistingPlantList());
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = ex.Message });
//            }
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetById(int id)
//        {
//            try
//            {
//                return Ok(QPrimaryService.GetPlant(id));
//            }
//            catch (Exception ex)
//            {
//                return NotFound(new { message = ex.Message });
//            }
//        }

//        [HttpPost]
//        public IActionResult Create(Plant plant)
//        {
//            try
//            {
//                plant.PlantId = 0;

//                var service = new DataModify();
//                int id = service.SaveBusiness(plant);

//                return StatusCode(201, QPrimaryService.GetPlant(id));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPut("{id}")]
//        public IActionResult Update(int id, Plant plant)
//        {
//            try
//            {
//                QPrimaryService.GetPlant(id);

//                plant.PlantId = id;

//                var service = new DataModify();
//                service.SaveBusiness(plant);

//                return Ok(QPrimaryService.GetPlant(id));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            try
//            {
//                using var context = new MasterPorterContext();

//                var plant = context.Plants
//                    .FirstOrDefault(x => x.PlantId == id);

//                if (plant == null)
//                    return NotFound();

//                context.Plants.Remove(plant);
//                context.SaveChanges();

//                return Ok(new
//                {
//                    message = "Plant deleted successfully."
//                });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }
//    }
//}

using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var plants = await _context.Plants
                    .AsNoTracking()
                    .OrderBy(x => x.PlantId)
                    .ToListAsync();

                return Ok(plants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while getting plants.",
                    error = ex.Message
                });
            }
        }

        // GET: api/Plant/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var plant = await _context.Plants
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PlantId == id);

                if (plant == null)
                {
                    return NotFound(new
                    {
                        message = $"Plant with ID {id} not found."
                    });
                }

                return Ok(plant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while getting plant.",
                    error = ex.Message
                });
            }
        }

        // POST: api/Plant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Plant plant)
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

                // Do not send existing ID while creating
                plant.PlantId = 0;

                _context.Plants.Add(plant);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = plant.PlantId },
                    plant
                );
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    message = "Unable to create plant.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while creating plant.",
                    error = ex.Message
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