using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;

namespace A5.Data.Service
{
    public class AwardTypeService : EntityBaseRepository<AwardType>, IAwardTypeService
    {
        public AwardTypeService(AppDbContext context) : base(context) { }
    }
}