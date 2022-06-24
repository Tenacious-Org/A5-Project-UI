using System.Collections.Generic;
using System.Linq;

namespace A5.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, IAudit,IEntityBase,  new()
    {
        public bool Create(T entity);
        public bool Disable(int id);
        public bool Update(T entity);
        public T GetById(int id);
        public IEnumerable<T> GetAll();

    }
}