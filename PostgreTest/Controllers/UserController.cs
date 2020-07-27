using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostgreTest.Dtos;
using PostgreTest.Services.Abstract;

namespace PostgreTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/api/v1/[controller]/GetUserById/{id:Guid}")]
        public async Task<IActionResult> GetUserById([FromHeader] Guid tenantId, [FromHeader] string token, [FromHeader] Guid transactionId, [FromHeader] Guid organizationId, Guid id)
        {
            var response = await _userService.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpDelete]
        [Route("/api/v1/[controller]/DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromHeader] Guid tenantId, [FromHeader] string token, [FromHeader] Guid transactionId, [FromHeader] Guid organizationId, [FromBody] UserDeleteDto dto)
        {
            var response = await _userService.DeleteAsync(dto);

            return Ok(response);
        }

        [HttpPost]
        [Route("/api/v1/[controller]/CreateUser")]
        public async Task<IActionResult> CreateUser([FromHeader] Guid tenantId, [FromHeader] string token, [FromHeader] Guid transactionId, [FromHeader] Guid organizationId, [FromBody] UserCreateDto dto)
        {
            var response = await _userService.AddAsync(dto);

            return Ok(response);
        }

        [HttpPut]
        [Route("/api/v1/[controller]/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromHeader] Guid tenantId, [FromHeader] string token, [FromHeader] Guid transactionId, [FromHeader] Guid organizationId, [FromBody] UserUpdateDto dto)
        {
            var response = await _userService.UpdateAsync(dto);

            return Ok(response);
        }
    }
}
