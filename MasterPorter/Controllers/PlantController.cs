using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var plants = _context.Plant
                    .OrderBy(x => x.PlantId)
                    .ToList();

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
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var plant = _context.Plant
                    .FirstOrDefault(x => x.PlantId == id);

                if (plant == null)
                {
                    return NotFound(new
                    {
                        message = "Plant not found."
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
        public IActionResult Create([FromBody] Plant plant)
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

                if (string.IsNullOrWhiteSpace(plant.PlantName))
                {
                    return BadRequest(new
                    {
                        message = "Plant Name is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantCode))
                {
                    return BadRequest(new
                    {
                        message = "Plant Code is required."
                    });
                }

                // Duplicate Plant Code check
                var existingCode = _context.Plant
                    .FirstOrDefault(x => x.PlantCode == plant.PlantCode);

                if (existingCode != null)
                {
                    return BadRequest(new
                    {
                        message = "Plant Code already exists."
                    });
                }

                // Default Status
                if (string.IsNullOrWhiteSpace(plant.Status))
                {
                    plant.Status = "Active";
                }

                plant.PlantId = 0;

                _context.Plant.Add(plant);
                _context.SaveChanges();

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = plant.PlantId },
                    plant
                );
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
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Plant plant)
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

                var existingPlant = _context.Plant
                    .FirstOrDefault(x => x.PlantId == id);

                if (existingPlant == null)
                {
                    return NotFound(new
                    {
                        message = "Plant not found."
                    });
                }

                // Duplicate Plant Code check
                var duplicateCode = _context.Plant
                    .FirstOrDefault(x =>
                        x.PlantCode == plant.PlantCode &&
                        x.PlantId != id);

                if (duplicateCode != null)
                {
                    return BadRequest(new
                    {
                        message = "Plant Code already exists."
                    });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantName))
                {
                    return BadRequest(new
                    {
                        message = "Plant Name is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(plant.PlantCode))
                {
                    return BadRequest(new
                    {
                        message = "Plant Code is required."
                    });
                }

                existingPlant.PlantName = plant.PlantName;
                existingPlant.PlantCode = plant.PlantCode;
                existingPlant.Status = string.IsNullOrWhiteSpace(plant.Status)
                    ? "Active"
                    : plant.Status;

                _context.SaveChanges();

                return Ok(existingPlant);
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var plant = _context.Plant
                    .FirstOrDefault(x => x.PlantId == id);

                if (plant == null)
                {
                    return NotFound(new
                    {
                        message = "Plant not found."
                    });
                }

                _context.Plant.Remove(plant);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Plant deleted successfully."
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