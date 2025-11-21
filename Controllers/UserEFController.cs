using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserEFController : ControllerBase
    {
        DataContextEF _entityFramework;
        IMapper _mapper;

        public UserEFController(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
            _mapper = new Mapper(new MapperConfiguration(cfg =>{
                cfg.CreateMap<UserToAddDto, User>();
            }));
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList();

            return users;
        }

        [HttpGet("GetSingleUser/{userId}")]
        public User GetSingleUser(int userId)
        {
            User? user = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault();

            if(user != null)
                return user;
            
            throw new Exception("Failed to Get User");
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            User? userDb = _entityFramework.Users
                .Where(u => u.UserId == user.UserId)
                .FirstOrDefault();

            if(userDb != null)
            {

                userDb.Active = user.Active;
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Gender = user.Gender;

                if (_entityFramework.SaveChanges() > 0)
                    return Ok();
            }
            
            throw new Exception("Failed to Update User");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserToAddDto user)
        {
            User userDb = _mapper.Map<User>(user);

            /*
            // Without auto mapper

            User userDb = new User();

            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            */

            _entityFramework.Add(userDb);

            if (_entityFramework.SaveChanges() > 0)
                return Ok();
            
            throw new Exception("Failed to Add User");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
           User? userDb = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault();

            if(userDb != null)
            {
                _entityFramework.Users.Remove(userDb);

                if (_entityFramework.SaveChanges() > 0)
                    return Ok();
            }
            
            throw new Exception("Failed to Delete User");
        }

        // USER SALARY

        [HttpGet("UserSalary/{userId}")]
        public UserSalary GetUserSalary(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();

            if (userSalary != null)
                return userSalary;

            throw new Exception("Failed to get User salary");
        }

        [HttpPut("UserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? userSalaryDb = _entityFramework.UserSalary.Where(u => u.UserId == userSalary.UserId).FirstOrDefault();

            if (userSalaryDb != null)
            {
                userSalaryDb.Salary = userSalary.Salary;

                if (_entityFramework.SaveChanges() > 0)
                    return Ok();
            }
                
            throw new Exception("Failed to edit User salary");
        }

        [HttpPost("UserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            _entityFramework.Add(userSalary);

            if (_entityFramework.SaveChanges() > 0)
                    return Ok();

            throw new Exception("Failed to add User salary");
        }

        [HttpDelete("UserSalary/{userId}")]
        public IActionResult DeleteUserSalary(int userId)
        {
            UserSalary? userSalaryDb = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault();

            if(userSalaryDb != null)
            {   
                _entityFramework.UserSalary.Remove(userSalaryDb);
                
                if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();    
                }
            }

            throw new Exception("Failed to delete User salary");
        }

        //UserJobInfo

        [HttpGet("UserJobInfo/{userId}")]
        public UserJobInfo GetUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();

            if (userJobInfo != null)
                return userJobInfo;

            throw new Exception("Failed to get userJobInfo");
        }

        [HttpPut("UserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo.Where(u => u.UserId == userJobInfo.UserId).FirstOrDefault();

            if (userJobInfoDb != null)
            {
                userJobInfoDb.JobTitle = userJobInfo.JobTitle;
                userJobInfoDb.Department = userJobInfo.Department;

                if (_entityFramework.SaveChanges() > 0)
                    return Ok();
            }
                
            throw new Exception("Failed to edit userJobInfo");
        }

        [HttpPost("UserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            _entityFramework.Add(userJobInfo);

            if (_entityFramework.SaveChanges() > 0)
                    return Ok();

            throw new Exception("Failed to add userJobInfo");
        }

        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault();

            if(userJobInfoDb != null)
            {   
                _entityFramework.UserJobInfo.Remove(userJobInfoDb);
                
                if(_entityFramework.SaveChanges() > 0)
                {
                    return Ok();    
                }
            }
            
            throw new Exception("Failed to delete User salary");
        }
    }
}