using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly StatusService _statusService;
        public StatusController(ILogger<StatusController> logger,StatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        } 
        [HttpGet("GetById")]
        public ActionResult GetByStatusId([FromQuery] int id)
        {
            try{
                var data = _statusService.GetById(id);
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
            
        }

            

    }
}
      
        
