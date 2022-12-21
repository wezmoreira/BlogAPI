using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel.Categories;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "O name é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O slug é obrigatório")]
    public string Slug { get; set; }
}