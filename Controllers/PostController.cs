using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModel;
using Blog.ViewModel.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    [HttpGet("v1/post")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var post = await context.Posts.ToListAsync();
            return Ok(new ResultViewModel<List<Post>>(post));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Post>("WM0X20"));
        }
    }

    [HttpGet("v1/post/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (post == null)
            return NotFound(new ResultViewModel<string>("Post não encontrado"));

        return Ok(new ResultViewModel<Post>(post));
    }

    [HttpPost("v1/post")]
    public async Task<IActionResult> PostAsync(
        [FromServices] BlogDataContext context,
        [FromBody] CreatePostViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Post>(ModelState.GetErrors()));

        try
        {
            var post = new Post
            {
                Author = model.Author,
                Title = model.Title,
                Summary = model.Summary,
                Body = model.Body,
                Slug = model.Slug,
                CreateDate = model.CreateDate,
                LastUpdateDate = model.LastUpdateDate,
                Category = model.Category,
                Tags = model.Tags
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();

            return Created($"v1/post/{post.Id}", new ResultViewModel<Post>(post));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Post>("WM0X21 - Não foi possível criar o objeto"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Post>("Erro interno no servidor"));
        }
    }

    [HttpPut("v1/post/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] BlogDataContext context,
        [FromBody] CreatePostViewModel model,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Post>(ModelState.GetErrors()));

        try
        {
            var postId = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (postId == null)
                return NotFound(new ResultViewModel<Post>("Não foi possivel encontrar o post"));

            postId.Author = model.Author;
            postId.Title = model.Title;
            postId.Summary = model.Summary;
            postId.Body = model.Body;
            postId.Slug = model.Slug;
            postId.CreateDate = model.CreateDate;
            postId.LastUpdateDate = model.LastUpdateDate;
            postId.Category = model.Category;
            postId.Tags = model.Tags;

            await context.Posts.AddAsync(postId);
            await context.SaveChangesAsync();

            return Created($"v1/post/{postId.Id}", new ResultViewModel<Post>(postId));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Post>("WM0X22 - Não foi possível atualizar o objeto"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Post>("Erro interno no servidor"));
        }
    }

    [HttpDelete("v1/post/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);

        if (post == null)
            return NotFound(new ResultViewModel<Post>("Post não encontrado"));

        try
        {
            context.Posts.Remove(post);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, "WM0X23 - Não foi possível deletar o objeto");
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor");
        }
    }
}