using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new MasterPorterContext())
                {
                    var machines = context.Machine
                        .Select(m => new
                        {
                            machineID = m.MachineID,
                            machineName = m.MachineName,
                            machineCode = m.MachineCode,
                            status = m.Status,
                            plantId = m.PlantId,
                            divisionId = m.DivisionId
                        })
                        .ToList();

                    return Ok(machines);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching machines.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }


        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid machine ID."
                    });
                }

                using (var context = new MasterPorterContext())
                {
                    var machine = context.Machine
                        .Where(m => m.MachineID == id)
                        .Select(m => new
                        {
                            machineID = m.MachineID,
                            machineName = m.MachineName,
                            machineCode = m.MachineCode,
                            status = m.Status,
                            plantId = m.PlantId,
                            divisionId = m.DivisionId
                        })
                        .FirstOrDefault();

                    if (machine == null)
                    {
                        return NotFound(new
                        {
                            message = "Machine not found."
                        });
                    }

                    return Ok(machine);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching machine.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }


        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public IActionResult Create([FromBody] Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    return BadRequest(new
                    {
                        message = "Machine data is required."
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                using (var context = new MasterPorterContext())
                {
                    // Duplicate Machine Name
                    bool nameExists = context.Machine.Any(x =>
                        x.MachineName.ToLower() ==
                        machine.MachineName.Trim().ToLower());

                    if (nameExists)
                    {
                        return Conflict(new
                        {
                            message = "Machine Name already exists."
                        });
                    }

                    // Duplicate Machine Code
                    bool codeExists = context.Machine.Any(x =>
                        x.MachineCode.ToLower() ==
                        machine.MachineCode.Trim().ToLower());

                    if (codeExists)
                    {
                        return Conflict(new
                        {
                            message = "Machine Code already exists."
                        });
                    }

                    machine.MachineID = 0;
                    machine.MachineName = machine.MachineName.Trim();
                    machine.MachineCode = machine.MachineCode.Trim();

                    context.Machine.Add(machine);

                    context.SaveChanges();

                    return Ok(new
                    {
                        message = "Machine created successfully.",
                        machineID = machine.MachineID
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while creating machine.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }


        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Machine machine)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid machine ID."
                    });
                }

                if (machine == null)
                {
                    return BadRequest(new
                    {
                        message = "Machine data is required."
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                using (var context = new MasterPorterContext())
                {
                    var existingMachine = context.Machine
                        .FirstOrDefault(x => x.MachineID == id);

                    if (existingMachine == null)
                    {
                        return NotFound(new
                        {
                            message = "Machine not found."
                        });
                    }

                    // Duplicate Name
                    bool nameExists = context.Machine.Any(x =>
                        x.MachineID != id &&
                        x.MachineName.ToLower() ==
                        machine.MachineName.Trim().ToLower());

                    if (nameExists)
                    {
                        return Conflict(new
                        {
                            message = "Machine Name already exists."
                        });
                    }

                    // Duplicate Code
                    bool codeExists = context.Machine.Any(x =>
                        x.MachineID != id &&
                        x.MachineCode.ToLower() ==
                        machine.MachineCode.Trim().ToLower());

                    if (codeExists)
                    {
                        return Conflict(new
                        {
                            message = "Machine Code already exists."
                        });
                    }

                    existingMachine.MachineName =
                        machine.MachineName.Trim();

                    existingMachine.MachineCode =
                        machine.MachineCode.Trim();

                    existingMachine.Status =
                        machine.Status;

                    existingMachine.PlantId =
                        machine.PlantId;

                    existingMachine.DivisionId =
                        machine.DivisionId;

                    context.SaveChanges();

                    return Ok(new
                    {
                        message = "Machine updated successfully.",
                        machineID = existingMachine.MachineID
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while updating machine.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }


        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid machine ID."
                    });
                }

                using (var context = new MasterPorterContext())
                {
                    var machine = context.Machine
                        .FirstOrDefault(x => x.MachineID == id);

                    if (machine == null)
                    {
                        return NotFound(new
                        {
                            message = "Machine not found."
                        });
                    }

                    context.Machine.Remove(machine);

                    context.SaveChanges();

                    return Ok(new
                    {
                        message = "Machine deleted successfully."
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while deleting machine.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }
    }
}