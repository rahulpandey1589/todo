using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDbContext _context;

        public UserRepository(TodoDbContext context)
        {
            _context = context;
        }


        public IQueryable<Users> GetAll()
        {
            return _context.Users;  // LINQ --Lamda operator
        }

    }
}
