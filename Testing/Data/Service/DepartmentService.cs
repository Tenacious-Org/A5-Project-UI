using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using A5.Data.Repository;
using A5.Validations;

namespace A5.Data.Service
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        public DepartmentService(AppDbContext context, MasterRepository master) : base(context) 
        {
            _context = context;
            _master = master;
        }
        

         public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
         { 
            DepartmentServiceValidations.ValidateGetByOrganisation(id);
            try
            {
                var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == id && nameof.IsActive == true).ToList();
                return organisationDetails;
            }
            catch(Exception exception)
            {
                throw exception;
            }
             
         }
         public IEnumerable<object> GetAllDepartments()
         {
            var department = _master.GetAllDepartments();
            return department.Select( Department => new{
                id = Department.Id,
                departmentName = Department.DepartmentName,
                organisationName = Department.Organisation.OrganisationName,
                isActive = Department.IsActive,
                addedBy = Department.AddedBy,
                addedOn = Department.AddedOn,
                updatedBy = Department.UpdatedBy,
                updatedOn = Department.UpdatedOn
            });
             
         }
    }
}