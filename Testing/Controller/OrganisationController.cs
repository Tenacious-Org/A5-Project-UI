using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;
using A5.Data;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly AppDbContext _context;
        private readonly OrganisationService _organisationService;
        public OrganisationController(ILogger<OrganisationController> logger, AppDbContext context, OrganisationService organisationService)
        {
            _logger = logger;
            _context = context;
            _organisationService = organisationService;
        } 

        /// <summary>
        ///  This Method is used to view all organisation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewOrganisation
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Organisation.
        /// </returns>

        [HttpGet("GetAll")]
        public ActionResult GetAllOrganisation()
        {
            try{
                var data = _organisationService.GetAll();
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
        
        /// <summary>
        ///  This Method is used to view single Organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

         [HttpGet("GetById")]
        public ActionResult GetByOrganisationId([FromQuery] int id)
        {
            try{
                var data = _organisationService.GetById(id);
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
        
        /// <summary>
        ///  This Method is used to create new organisation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateOrganisation
        ///     {
        ///        "OrganisationName" = "Development",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="organisation">String</param>
        /// <returns>
        ///Return "Organisation Added Successfully" when the Organisation is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Organisation organisation)
        {
            try
            {    
                var data = _organisationService.Create(organisation);
                return Ok("Organisation Created.");
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

        /// <summary>
        ///  This Method is used to view single Organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="organisation">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Organisation organisation)
        {
            try{
                var data = _organisationService.Update(organisation);           
                return Ok("Organisation Updated.");          
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

        /// <summary>
        ///  This Method is used to disable the organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return "Organisation Disabled Successfully" message when the isactive filed is set to 0 otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            try
            {
                var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList().Count();
                if(checkEmployee > 0)
                {             
                    return Ok(checkEmployee);
                    // return Ok ( new{Message= "There are certain Employees in that Organisation. You're gonna change their Organisation to disable this Organisation." } );
                }
                else
                {
                    var data = _organisationService.Disable(id);
                    return Ok(data);
                }
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