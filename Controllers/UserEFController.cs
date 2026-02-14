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
        IUserRepository _userRepository;
        IMapper _mapper;

        public UserEFController(IConfiguration config, IUserRepository userRepository)
        {
            _userRepository = userRepository;

            _mapper = new Mapper(new MapperConfiguration(cfg =>{
                cfg.CreateMap<UserToAddDto, User>();
            }));
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _userRepository.GetUsers();
            return users;
        }

        [HttpGet("GetSingleUser/{userId}")]
        public User GetSingleUser(int userId)
        {
            return _userRepository.GetSingleUser(userId);
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            User? userDb = _userRepository.GetSingleUser(user.UserId);

            if(userDb != null)
            {

                userDb.Active = user.Active;
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Gender = user.Gender;

                if (_userRepository.SaveChanges())
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

            _userRepository.AddEntity(userDb);

            if (_userRepository.SaveChanges())
                return Ok();
            
            throw new Exception("Failed to Add User");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
           User? userDb = _userRepository.GetSingleUser(userId);

            if(userDb != null)
            {
                _userRepository.RemoveEntity(userDb);

                if (_userRepository.SaveChanges())
                    return Ok();
            }
            
            throw new Exception("Failed to Delete User");
        }

        // USER SALARY

        [HttpGet("UserSalary/{userId}")]
        public UserSalary GetUserSalary(int userId)
        {
            return _userRepository.GetSingleUserSalary(userId);
        }

        [HttpPut("UserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? userSalaryDb = _userRepository.GetSingleUserSalary(userSalary.UserId);

            if (userSalaryDb != null)
            {
                userSalaryDb.Salary = userSalary.Salary;

                if (_userRepository.SaveChanges())
                    return Ok();
            }
                
            throw new Exception("Failed to edit User salary");
        }

        [HttpPost("UserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            _userRepository.AddEntity(userSalary);

            if (_userRepository.SaveChanges())
                    return Ok();

            throw new Exception("Failed to add User salary");
        }

        [HttpDelete("UserSalary/{userId}")]
        public IActionResult DeleteUserSalary(int userId)
        {
            UserSalary? userSalaryDb = _userRepository.GetSingleUserSalary(userId);

            if(userSalaryDb != null)
            {   
                _userRepository.RemoveEntity(userSalaryDb);
                
                if(_userRepository.SaveChanges())
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
            return _userRepository.GetSingleUserJobInfo(userId);
        }

        [HttpPut("UserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userJobInfo.UserId);

            if (userJobInfoDb != null)
            {
                userJobInfoDb.JobTitle = userJobInfo.JobTitle;
                userJobInfoDb.Department = userJobInfo.Department;

                if (_userRepository.SaveChanges())
                    return Ok();
            }
                
            throw new Exception("Failed to edit userJobInfo");
        }

        [HttpPost("UserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            _userRepository.AddEntity(userJobInfo);

            if (_userRepository.SaveChanges())
                    return Ok();

            throw new Exception("Failed to add userJobInfo");
        }

        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userId);

            if(userJobInfoDb != null)
            {   
                _userRepository.RemoveEntity(userJobInfoDb);
                
                if(_userRepository.SaveChanges())
                {
                    return Ok();    
                }
            }
            
            throw new Exception("Failed to delete User salary");
        }
    }
}