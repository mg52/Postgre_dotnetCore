using PostgreTest.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreTest.Services.Abstract
{
    public interface IUserService
    {
        Task<UserDto> AddAsync(UserCreateDto dto);
        Task<UserDto> UpdateAsync(UserUpdateDto dto);
        Task<UserDto> GetByIdAsync(Guid id);
        //Task<Paged<UserDto>> GetAllAsync(UserMovementTypeFilterDto dto);
        Task<UserDto> DeleteAsync(UserDeleteDto dto);
    }
}
