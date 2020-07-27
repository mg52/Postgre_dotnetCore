using PostgreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreTest.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<User> GetByIdAsync(Guid id);
    }
}
