using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John", Email = "john@test.com", Department = "IT" },
            new User { Id = 2, Name = "Sara", Email = "sara@test.com", Department = "HR" }
        };

        // GET all users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        // GET user by ID
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        // POST add new user
        [HttpPost]
        public ActionResult<User> AddUser(User newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                users.Add(newUser);

                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the user.");
            }
        }

        // PUT update user
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Department = updatedUser.Department;

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // DELETE user
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                users.Remove(user);

                return Ok("User deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}