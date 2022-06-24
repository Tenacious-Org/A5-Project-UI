using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;

namespace A5.Data.Service.Interfaces
{
    public interface IDesignationService : IEntityBaseRepository<Designation>
    {
         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id);
    }
}