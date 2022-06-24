using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using A5.Validations;

namespace A5.Data.Service
{
    public class StatusService {
        private readonly AppDbContext _context;
        public StatusService(AppDbContext context) 
        { 
            _context=context;
        }
        public Status GetById(int id)
        {
            StatusServiceValidations.ValdiateGetById(id);
            try
            {
                return _context.Set<Status>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
    }
        
        
    
}