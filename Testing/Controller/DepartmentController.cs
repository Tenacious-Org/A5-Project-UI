using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;
using A5.Data;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly AppDbContext _context;
        private readonly DepartmentService _departmentService;
        public DepartmentController(ILogger<DepartmentController> logger, AppDbContext context,DepartmentService departmentService)
        {
            _logger= logger;
            _context = context;
            _departmentService = departmentService;
        }

        /// <summary>
        ///  This Method is used to view all department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewDepartment
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Departments.
        /// </returns>
       
        [HttpGet("GetAll")]
        public ActionResult GetAllDepartment()
        {
            try
            {
                var result = _departmentService.GetAllDepartments();
                return Ok(result);
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
        ///  This Method is used to view All departments which are comes under one organisation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewDepartmentsByOrganisationId
        ///     {
        ///        "OrganisationId" = "1",    
        ///        "DepartmentId" = "3",
        ///        "DepartmentId" = "4",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns List of Departments from OrganisationId
        /// </returns>

        [HttpGet("GetDepartmentsByOrganisationId")]
        public ActionResult GetDepartmentsByOrganisationId(int id)
        {
            try{
                var data = _departmentService.GetDepartmentsByOrganisationId(id);
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
        ///  This Method is used to view single Departmnet by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleDepartment
        ///     {
        ///        "DepartmentId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle Department by id
        /// </returns>

        [HttpGet("GetById")]
        public ActionResult GetByDepartmentId([FromQuery] int id)
        {
            try{
                var data = _departmentService.GetById(id);
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
        ///  This Method is used to create new department under corresponding organisation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateDepartment
        ///     {
        ///        "OrganisationName" = "Development",
        ///        "DepartmentName" = "Dotnet",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="department">String</param>
        /// <returns>
        ///Return "Department Added Successfully" when the Department is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Department department)
        {
            try{
                var data = _departmentService.Create(department);
                return Ok("Created.");
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
        /// <param name="department">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Department department)
        {
            try{
                var data = _departmentService.Update(department);
                return Ok("Updated.");
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
        ///  This Method is used to disable the Department by id from OrganisationId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableDepartment
        ///     {
        ///        "DepartmentId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return "Department Disabled Successfully" message when the isactive filed is set to 0 otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            try{
                 var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.DepartmentId== id).ToList().Count();
                if(checkEmployee>0){
                    return Ok(checkEmployee);
                }else{
                    var data = _departmentService.Disable(id);
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