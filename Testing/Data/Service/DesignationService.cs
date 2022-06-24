using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using A5.Data.Repository;
using A5.Validations;
namespace A5.Data.Service
{
    public class DesignationService : EntityBaseRepository<Designation>, IDesignationService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        public DesignationService(AppDbContext context, MasterRepository master) : base(context)
        {
            _context = context;
            _master = master;
        }

         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id)
         {
             DesignationServiceValidations.ValidateGetByDepartment(id);
            try
            {
                var data =  _context.Set<Designation>().Where(nameof =>nameof.DepartmentId == id && nameof.IsActive == true).ToList();
                var count = data.Count();
                if(count != 0)
                {
                    return data;
                }
                else
                {
                    throw new ValidationException(" Department not Found!! ");
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
             
         }
         public IEnumerable<object> GetAllDesignations()
         {
            var designation = _master.GetAllDesignation();
            return designation.Select( Designation => new{
                id = Designation.Id,
                designationName = Designation.DesignationName,
                departmentName = Designation.Department.DepartmentName,
                isActive = Designation.IsActive,
                addedBy = Designation.AddedBy,
                addedOn = Designation.AddedOn,
                updatedBy = Designation.UpdatedBy,
                updatedOn = Designation.UpdatedOn
            });
         }
    }
    
}