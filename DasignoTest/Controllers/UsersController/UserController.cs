using DasignoTest.DTOs.userDTOs;
using DasignoTest.Entitys.Users;
using DasignoTest.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace DasignoTest.Controllers.UsersController
{
    [ApiController]
    [Route("Api/Users")]
    public class UserController : ControllerBase
    {

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }
        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUserList([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery(Name = "pageSize")] int pageSize = 10)
        {
            var serviceResponse = await UserService.GetUserList(pageNumber, pageSize);
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(serviceResponse.status, serviceResponse);
            }
        }
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetUserById([FromQuery(Name = "Id")] int Id)
        {
            var serviceResponse = await UserService.GetUserById(Id);
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(serviceResponse.status, serviceResponse);
            }
        }

        [HttpGet]
        [Route("getByParam")]
        public async Task<IActionResult> GetUsersByParam([FromQuery(Name = "Search Param")] string param, [FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery(Name = "pageSize")] int pageSize = 5)
        {
            var serviceResponse = await UserService.GetUsersbyParams(param, pageNumber, pageSize);
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(500, serviceResponse);
            }
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUser)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }
            var serviceResponse = await UserService.CreateUser(createUser);
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(serviceResponse.status, serviceResponse);
            }
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery(Name = "UserId")] int userId , [FromBody] UpdateUserDTO updateUser)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            var serviceResponse = await UserService.UpdateUser(userId,updateUser);
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(serviceResponse.status, serviceResponse);
            }
        }
        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery (Name = "UserId")] int userId)
        {
            var serviceResponse = await UserService.DeleteUserById(userId);
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(serviceResponse.status, serviceResponse);
            }
        }

        [HttpDelete]
        [Route("massivedelete")]
        public async Task<IActionResult> MassiveDelete()
        {
            var serviceResponse = await UserService.MassiveDelete();
            switch (serviceResponse.status)
            {
                case 200:
                    return Ok(serviceResponse.data);
                default:
                    return StatusCode(serviceResponse.status, serviceResponse);
            }
        }
    }
}
