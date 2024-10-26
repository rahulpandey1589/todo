using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;

namespace Todo.Persistence.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<Users> GetAll();
    }
}
