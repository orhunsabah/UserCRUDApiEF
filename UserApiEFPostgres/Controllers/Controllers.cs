using Microsoft.AspNetCore.Mvc;
using UserApiEFPostgres.Models;
using UserApiEFPostgres.Repo;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepo _userRepo;

    public UserController(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userRepo.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] UserModel user)
    {
        await _userRepo.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserById(int id, [FromBody] UserModel user)
    {
        var userToUpdate = await _userRepo.GetUserByIdAsync(id);
        if (userToUpdate == null)
            return NotFound();

        userToUpdate.Name = user.Name;
        userToUpdate.Email = user.Email;

        await _userRepo.UpdateUserByIdAsync(id, userToUpdate);

        return Ok(userToUpdate);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userRepo.DeleteUserAsync(id);
        return Ok();
    }
}