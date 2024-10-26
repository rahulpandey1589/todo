using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;

namespace TodoAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsers();
    }
}
