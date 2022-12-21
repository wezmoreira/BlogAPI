using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;
using Blog.ViewModel.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts")]
    public async Task<IActionResult> Post(
        [FromServices] BlogDataContext context,
        [FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-")
        };

        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password
            }));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<string>("WM0X99 - Este email já esta cadastrado"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("WM0X98 - Falha interna no servidor"));
        }
    }


    [HttpPost("v1/login")]
    public async Task<IActionResult> Login([FromServices] TokenServices tokenServices,
        [FromBody] LoginViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha invalidos"));

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha invalidos"));


        try
        {
            var token = tokenServices.GenerateToken(user);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor"));
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/teste")]
    public IActionResult TesteGet()
    {
        return Ok(User.Identity.Name);
    }
}