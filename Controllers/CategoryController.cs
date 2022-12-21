using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModel;
using Blog.ViewModel.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(new ResultViewModel<List<Category>>(categories));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X07 - Erro interno no servidor"));
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var categories = await context
            .Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categories == null)
        {
            return NotFound(new ResultViewModel<Category>("Id não encontrado"));
        }

        return Ok(new ResultViewModel<Category>(categories));
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context,
        [FromBody] CreateCategoryViewModel category)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        
        try
        {
            var categories = new Category
            {
                Name = category.Name,
                Slug = category.Slug.ToLower()
            };
            
            await context.Categories.AddAsync(categories);
            await context.SaveChangesAsync();
            
            return Created($"v1/categories/{categories.Id}", new ResultViewModel<Category>(categories));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X01 - Não foi possível criar o objeto"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X02 - Erro interno no servidor"));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id,
        [FromBody] CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        
        try
        {
            var categories = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (categories == null)
                return NotFound(new ResultViewModel<Category>("Id não encontrado"));

            categories.Name = model.Name;
            categories.Slug = model.Slug.ToLower();
            
            context.Categories.Update(categories);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<Category>(categories));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X03 - Não foi possível atualizar o objeto"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X04 - Erro interno no servidor"));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> Delete([FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var categories = await context
            .Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categories == null)
            return NotFound(new ResultViewModel<Category>("Id não encontrado"));

        try
        {
            context.Categories.Remove(categories);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X05 - Não foi possível deletar o objeto"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("WM0X06 - Erro interno no servidor"));
        }

        return Ok(new ResultViewModel<Category>(categories));
    }
}