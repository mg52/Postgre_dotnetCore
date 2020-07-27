using Mapster;
using PostgreTest.Dtos;
using PostgreTest.Models;
using PostgreTest.Repositories.Abstract;
using PostgreTest.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreTest.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepositories;

        public UserService(IUserRepository userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public async Task<UserDto> AddAsync(UserCreateDto dto)
        {
            var entity = dto.Adapt<User>();
            await _userRepositories.AddAsync(entity);
            var result = entity.Adapt<UserDto>();
            return result;
        }

        public async Task<UserDto> UpdateAsync(UserUpdateDto dto)
        {
            var entity = await _userRepositories.GetByIdAsync(dto.Id);
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == nameof(dto.Name) && property.PropertyType == typeof(string))
                {
                    property.SetValue(entity, dto.Name);
                }
                if (property.Name == nameof(dto.Email) && property.PropertyType == typeof(string))
                {
                    property.SetValue(entity, dto.Email);
                }
            }

            await _userRepositories.UpdateAsync(entity);

            var result = entity.Adapt<UserDto>();

            return result;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var entity = await _userRepositories.GetByIdAsync(id);

            var result = entity.Adapt<UserDto>();

            return result;
        }

        //public async Task<Paged<UserDto>> GetAllAsync(UserFilterDto dto)
        //{
        //    var entity = await _userRepositories.GetAllAsync(dto);

        //    var result = entity.Adapt<Paged<UserDto>>();

        //    return result;
        //}

        public async Task<UserDto> DeleteAsync(UserDeleteDto dto)
        {
            var entity = await _userRepositories.GetByIdAsync(dto.Id);

            await _userRepositories.DeleteAsync(entity);

            return new UserDto();
        }
    }
}
