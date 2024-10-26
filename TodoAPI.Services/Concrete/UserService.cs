using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Todo.Persistence.Domain;
using Todo.Persistence.Interfaces;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<Users>> GetAllUsers()
        {

            return await _userRepository.GetAll().ToListAsync();
        }

    }
}
