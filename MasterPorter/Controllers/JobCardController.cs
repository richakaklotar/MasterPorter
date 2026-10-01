using AutoEntity.EntityModels;
using Microsoft.AspNetCore.Mvc;
using ModifyService;
using ReadService;

namespace MasterPorterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobCardController : ControllerBase
    {
        #region Get All Job Cards

        [HttpGet("GetJobCardList")]
        public ActionResult<List<JobCard>> GetJobCardList()
        {
            try
            {
                var jobCards = QPrimaryService.GetJobCardList();

                return Ok(new
                {
                    message = "Job Cards fetched successfully.",
                    data = jobCards
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error while getting Job Cards.",
                    error = ex.Message
                });
            }
        }

        #endregion


        #region Get Job Card By ID

        [HttpGet("GetJobCard/{id}")]
        public ActionResult<JobCard> GetJobCard(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid JobCardID."
                    });
                }

                var jobCard = QPrimaryService.GetJobCard(id);

                return Ok(new
                {
                    message = "Job Card fetched successfully.",
                    data = jobCard
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error while getting Job Card.",
                    error = ex.Message
                });
            }
        }

        #endregion


        #region Save Job Card

        [HttpPost("SaveJobCard")]
        public ActionResult SaveJobCard([FromBody] JobCard jobCard)
        {
            try
            {
                if (jobCard == null)
                {
                    return BadRequest(new
                    {
                        message = "Job Card data is required."
                    });
                }

                var dataModify = new DataModify();

                int jobCardId = dataModify.SaveJobCard(jobCard);

                if (jobCardId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Job Card could not be saved."
                    });
                }

                return Ok(new
                {
                    message = jobCard.JobCardID > 0
                        ? "Job Card updated successfully."
                        : "Job Card created successfully.",
                    jobCardId = jobCardId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error while saving Job Card.",
                    error = ex.Message
                });
            }
        }

        #endregion


        #region Delete Job Card

        [HttpDelete("DeleteJobCard/{id}")]
        public ActionResult DeleteJobCard(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid JobCardID."
                    });
                }

                var dataModify = new DataModify();

                bool deleted = dataModify.DeleteJobCard(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Job Card not found."
                    });
                }

                return Ok(new
                {
                    message = "Job Card deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error while deleting Job Card.",
                    error = ex.Message
                });
            }
        }

        #endregion
    }
}