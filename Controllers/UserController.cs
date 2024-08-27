using Microsoft.AspNetCore.Mvc;
using ProdeApi.Models;
using ProdeApi.Services;

namespace ProdeApi.Controllers;


[Controller]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService userService)
    {
        _service = userService;
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return await _service.GetAsync();
    }

    [HttpPost]
    public async Task<User> AddUser([FromBody] User user)
    {
        await _service.CreateAsync(user);
        return user;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    
    
    

}