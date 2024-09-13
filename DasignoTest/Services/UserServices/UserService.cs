using DasignoTest.AuxModels.AuxResponseModels;
using DasignoTest.DBContext;
using DasignoTest.DTOs.userDTOs;
using DasignoTest.Entitys.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DasignoTest.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDBContext dBContext;

        public UserService(AppDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        #region GetUserList
        public async Task<CommonResponse<List<Users>>> GetUserList(int pageNumber, int pageSize)
        {
            var totalRecords = await dBContext.users.CountAsync();
            var userList = await dBContext.users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (userList.Count != 0)
            {
                return new CommonResponse<List<Users>>
                {
                    status = 200,
                    message = "List of users successfully obtained",
                    data = userList,
                    totalRecords = totalRecords,
                    totalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
                };
            }
            return new CommonResponse<List<Users>>
            {
                status = 500,
                message = "Could'nt get the user list"
            };
        }
        #endregion
        #region GetUserById
        public async Task<CommonResponse<Users>> GetUserById(int userid)
        {
            var user = await dBContext.users.FirstOrDefaultAsync(u => u.userId == userid);
            if (user == null)
            {
                return new CommonResponse<Users>
                {
                    status = 400,
                    message = $"There are not users recorded with this Id: {userid}"
                };
            }
            return new CommonResponse<Users>
            {
                status = 200,
                message = $"User successfully geted",
                data = user

            };
        }
        #endregion
        #region GetUsersbyParams
        public async Task<CommonResponse<List<Users>>> GetUsersbyParams(string paramFilter, int pageNumber, int pageSize)
        {
            var user = await dBContext.users.Where(u => u.name.Contains(paramFilter) || u.surName.Contains(paramFilter)).Skip((pageNumber -1)*pageSize).Take(pageSize).ToListAsync();
            if(user == null)
            {
                return new CommonResponse<List<Users>>
                {
                    status = 400,
                    message = $"there are not users recorded with this key word: {paramFilter}"
                };
            }
            return new CommonResponse<List<Users>>
            {
                status = 200,
                message = "User geted succesfully",
                data = user
            };
        }
        #endregion
        #region CreateUser
        public async Task<CommonResponse<object>> CreateUser(CreateUserDTO createUser)
        {
            createUser.name = CapitalizeFirstLetter(createUser.name);
            createUser.secondName = CapitalizeFirstLetter(createUser.secondName);
            createUser.surName = CapitalizeFirstLetter(createUser.surName);
            createUser.secondSurName = CapitalizeFirstLetter(createUser.secondSurName);

            var existedUser = await dBContext.users.FirstOrDefaultAsync(u => u.name == createUser.name && u.surName == createUser.surName);
            if (existedUser != null)
            {
                return new CommonResponse<object>
                {
                    status = 400,
                    message = "the user to create can't have a salary equal to 0",

                };
            }
                if (createUser.salary > 0)
                {
                    
                    var newUser = new Users
                    {
                        name = createUser.name,
                        secondName = createUser.secondName,
                        surName = createUser.surName,
                        secondSurName = createUser.secondSurName,
                        birthDate = createUser.birthDate,
                        salary = createUser.salary,
                        creationDate = DateTime.Now,

                    };

                    await dBContext.users.AddAsync(newUser);
                    var result = await dBContext.SaveChangesAsync();
                    if (result != 0)
                    {
                        return new CommonResponse<object>
                        {
                            status = 200,
                            message = "User created Succesfully"
                        };
                    }
                    else
                    {
                        return new CommonResponse<object>
                        {
                            status = 500,
                            message = " there was an error"
                        };
                    }

                }
                else
                {
                return new CommonResponse<object> {
                    status = 400,
                    message = "the salary can't be 0"
                };
                }
        }
        
        
        #endregion
        #region MassiveCreateUser
        public async Task<CommonResponse<object>> MassiveCreateUser(List<CreateUserDTO> createUsers)
        {
            List<Users> enableToCreate = new List<Users>();
            List<CreateUserDTO> conflictToCreate = new List<CreateUserDTO>();
            List<CreateUserDTO> disableToCreate = new List<CreateUserDTO>();
            foreach (var user in createUsers)
            {
                var existedUser = await dBContext.users.FirstOrDefaultAsync(eu => eu.name == user.name && eu.surName == user.surName);
                if (existedUser == null)
                {
                    if (user.salary > 0)
                    {
                        var enableUser = new Users
                        {
                            name = user.name,
                            secondName = user.secondName,
                            surName = user.surName,
                            secondSurName = user.secondSurName,
                            birthDate = user.birthDate,
                            salary = user.salary,
                            creationDate = DateTime.Now,
                        };

                        enableToCreate.Add(enableUser);
                    }
                    else
                    {
                        disableToCreate.Add(user);
                    }

                }
                else
                {
                    conflictToCreate.Add(user);
                }
            }
            await dBContext.AddRangeAsync(enableToCreate);
            var result = await dBContext.SaveChangesAsync();
            if (result != 0)
            {
                if (conflictToCreate.Any())
                {
                    return new CommonResponse<object>
                    {
                        status = 200,
                        message = "the next user could'nt be create cause them already exist",
                        data = conflictToCreate
                    };
                }
                else if (disableToCreate.Any())
                {
                    return new CommonResponse<object>
                    {
                        status = 200,
                        message = "the next user could'nt be create cause them can have a salary equal to 0 ",
                        data = disableToCreate
                    };
                }
                else
                {
                    return new CommonResponse<object>
                    {
                        status = 200,
                        message = "All new user created successfully",
                    };

                }
            }
            else
            {
                return new CommonResponse<object>
                {
                    status = 400,
                    message = "there was an error"
                };
            }
        }
        #endregion
        #region UpdateUser
        public async Task<CommonResponse<object>> UpdateUser(int userId,UpdateUserDTO updateUser)
        {
            updateUser.name = CapitalizeFirstLetter(updateUser.name);
            updateUser.secondName = CapitalizeFirstLetter(updateUser.secondName);
            updateUser.surName = CapitalizeFirstLetter(updateUser.surName);
            updateUser.secondSurName = CapitalizeFirstLetter(updateUser.secondSurName);
            var existedUser = await dBContext.users.FirstOrDefaultAsync(ue => ue.name == updateUser.name && ue.surName == updateUser.surName);
            if (existedUser == null)
            {
                var userChoesed = await dBContext.users.FindAsync(userId);
                if (userChoesed != null)
                {
                    userChoesed.name = updateUser.name??userChoesed.name;
                    userChoesed.secondName = updateUser.secondName??userChoesed.secondName;
                    userChoesed.surName = updateUser.surName??userChoesed.surName;
                    userChoesed.secondSurName = updateUser.secondSurName??userChoesed.secondSurName;
                    userChoesed.birthDate = updateUser.birthDate??userChoesed.birthDate;
                    userChoesed.salary = updateUser.salary??userChoesed.salary;
                    userChoesed.modifiedDate = DateTime.Now;
                    dBContext.Update(userChoesed);
                    var result = await dBContext.SaveChangesAsync();
                    if (result != 0)
                    {
                        return new CommonResponse<object>
                        {
                            status = 200,
                            message = "User updated succesfully",
                            data = userChoesed
                        };
                    }
                    return new CommonResponse<object>
                    {
                        status = 304,
                        message = "no changes was saved"
                    };
                }
                return new CommonResponse<object>
                {
                    status = 404,
                    message = "user not found"
                };
            }
            return new CommonResponse<object>
            {
                status = 409,
                message = "there's already an user with this information",
                data = updateUser
            };
            
        }
        #endregion
        #region DeleteUserById
        public async Task<CommonResponse<object>> DeleteUserById(int userId)
        {
            var userChoesed = await dBContext.users.FindAsync(userId);
            if(userChoesed == null) 
            {
                return new CommonResponse<object>
                {
                    status = 404,
                    message = $"there are not users recorded with this id {userId}"
                };
            }
            dBContext.users.Remove(userChoesed);
            var result = await dBContext.SaveChangesAsync();
            if (result == 0)
            {
                return new CommonResponse<object> {
                    status = 500,
                    message = "There was an error"
                };
            }
            return new CommonResponse<object> {
                status = 200,
                message = "User deleted succesfully"
            };
            
        }
        #endregion
        #region MassiveDelete
        public async Task<CommonResponse<object>> MassiveDelete()
        {
            dBContext.users.RemoveRange(dBContext.users);
            var result = await dBContext.SaveChangesAsync();
            if (result != 0)
            {
                try
                {
                    await dBContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('users', RESEED, 0)");
                }
                catch (Exception ex)
                {
                    
                    return new CommonResponse<object>
                    {
                        status = 500,
                        message = $"All users recorded deleted, but error resetting identity: {ex.Message}"
                    };
                }
                return new CommonResponse<object>
                {
                    status = 200,
                    message = "All users recorded deleted"
                };
            }
            return new CommonResponse<object>
            {
                status = 500,
                message = "There was an error"
            };
        } 
        #endregion


        private static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
