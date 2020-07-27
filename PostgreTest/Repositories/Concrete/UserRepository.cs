using Microsoft.EntityFrameworkCore;
using PostgreTest.Models;
using PostgreTest.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreTest.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly MyWebApiContext _ctx;

        public UserRepository(MyWebApiContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(User entity)
        {
            _ctx.Users.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _ctx.Users.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Users
                .Where(x => x.Id == id).Include(g => g.Group)
                .FirstOrDefaultAsync();
            return result;
        }
        
        public async Task UpdateAsync(User entity)
        {
            _ctx.Users.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
