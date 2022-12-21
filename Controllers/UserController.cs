using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModel;
using Blog.ViewModel.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("v1/user")]
    public async Task<IActionResult> GetAsync(
        [FromServices] BlogDataContext context)
    {
        try
        {
            var user = await context.Users.ToListAsync();

            return Ok(new ResultViewModel<List<User>>(user));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<User>("WM0X30 - Não foi possível recuperar o objeto"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<User>("Erro interno no serivdor"));
        }
    }

    [HttpGet("v1/user/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var userId = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (userId == null)
            return NotFound(new ResultViewModel<User>("Usuario não encontrado"));

        return Ok(new ResultViewModel<User>(userId));
    }

    [HttpPut("v1/user/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id,
        [FromBody] CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

        var userId = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (userId == null)
            NotFound(new ResultViewModel<User>("Usuario não encontrado"));

        userId.Name = model.Name;
        userId.Email = model.Email;
        userId.PasswordHash = model.PasswordHash;
        //userId.Image = model.Image;
        userId.Slug = model.Slug;
        userId.Bio = model.Bio;
        //userId.Posts = model.Posts;
        //userId.Roles = model.Roles;

        context.Update(userId);
        await context.SaveChangesAsync();

        return Ok(new ResultViewModel<User>(userId));
    }

    [HttpDelete("v1/user/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<User>("WM0X33 - Não foi possivel deletar o usuario"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<User>("WM0X32 - Erro interno no servidor"));
        }
    }
}