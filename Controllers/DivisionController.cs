using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebApi.Models;
using WebApi.Repositories.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private DivisionRepository divisionRepository;

        public DivisionController(DivisionRepository divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = divisionRepository.Get();
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode=200,
                        Message="Data Not Found"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Found",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            try
            {
                var data = divisionRepository.GetById(Id);
                if (data == null)
                {
                    return Ok(new 
                    { 
                        StatusCode = 200,
                        Message = "Data Not Found" 
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Found",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult Create(Division division)
        {
            try
            {
                var data = divisionRepository.Create(division);
                if (data == 0)
                {
                    return Ok(new { 
                        StatusCode = 200,
                        Message = "Data Failed To Save" 
                    });
                }
                else
                {
                    return Ok(new 
                    {
                        StatusCode = 200,
                        Message = "Data Saved Successfully",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public ActionResult Update(Division division)
        {
            try
            {
                var data = divisionRepository.Update(division);
                if (data == 0)
                {
                    return Ok(new 
                    { 
                        StatusCode = 200,
                        Message = "Data Failed To Update" 
                    });
                }
                else
                {
                    return Ok(new { 
                        StatusCode =200,
                        Message = "Data Updated Successfully",
                        Data = data
                    });
                }   
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                var data = divisionRepository.Delete(Id);
                if (data == 0)
                {
                    return Ok(new 
                    {
                        StatusCode = 200,
                        Message = "Data Failed To Delete" 
                    });
                }
                else
                {
                    return Ok(new 
                    { 
                        StatusCode = 200,
                        Message = "Data Deleted Successfully",
                        Data = data
                    });
                } 
            }
            catch (Exception ex)
            {

                return BadRequest(new 
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
