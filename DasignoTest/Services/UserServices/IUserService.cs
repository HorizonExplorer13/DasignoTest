using DasignoTest.AuxModels.AuxResponseModels;
using DasignoTest.DTOs.userDTOs;
using DasignoTest.Entitys.Users;

namespace DasignoTest.Services.UserServices
{
    public interface IUserService
    {
        Task<CommonResponse<object>> CreateUser(CreateUserDTO createUser);
        Task<CommonResponse<object>> DeleteUserById(int userId);
        Task<CommonResponse<Users>> GetUserById(int userid);
        Task<CommonResponse<List<Users>>> GetUserList(int pageNumber, int pageSize);
        Task<CommonResponse<List<Users>>> GetUsersbyParams(string paramFilter, int pageNumber, int pageSize);
        Task<CommonResponse<object>> MassiveCreateUser(List<CreateUserDTO> createUsers);
        Task<CommonResponse<object>> MassiveDelete();
        Task<CommonResponse<object>> UpdateUser(int userId, UpdateUserDTO updateUser);
    }
}
