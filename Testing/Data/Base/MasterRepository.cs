using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class MasterRepository
    {
        private AppDbContext _context;
        public MasterRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            try
            {
                var departments = _context.Set<Department>().Where(nameof =>nameof.IsActive == true).Include("Organisation").ToList();
                return departments;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Designation> GetAllDesignation()
        {
            try
            {
                var designations = _context.Set<Designation>().Where(nameof =>nameof.IsActive == true).Include("Department").ToList();
                return designations;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.ReportingPersonId != null && nameof.HRId != null && nameof.IsActive == true)
                    .ToList();
                return employee;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Award> GetAllAwardsList()
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .ToList();
                return award;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

         public Award GetAwardById(int id)
        {
            try{
                var award=_context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .FirstOrDefault(nameof=> nameof.Id == id);
                return award;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.ReportingPersonId != null && nameof.HRId != null && nameof.IsActive == true)
                    .FirstOrDefault(nameof =>nameof.Id == id);
                return employee;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public IEnumerable<Comment> GetComments(int awardId)
        {
           try
           {
               var comments= _context.Set<Comment>()
                    .Include("Employees")
                    .Include("Awards")
                    .Where(nameof=>nameof.AwardId==awardId)
                    .ToList();
               return comments;
           }
           catch(Exception exception){
               throw exception;
           }
        }
    }
}