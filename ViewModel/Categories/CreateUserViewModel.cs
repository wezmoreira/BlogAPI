using System.ComponentModel.DataAnnotations;
using Blog.Models;

namespace Blog.ViewModel.Categories;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O email é obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O passwwword é obrigatório")]
    public string PasswordHash { get; set; }
    [Required(ErrorMessage = "O slug é obrigatório")]
    public string Slug { get; set; }
    [Required(ErrorMessage = "A bio é obrigatório")]
    public string Bio { get; set; }
}