using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel.Accounts;

public class LoginViewModel
{
    [Required(ErrorMessage = "O password é obrigatório!")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "O E-mail é obrigatório!")]
    [EmailAddress(ErrorMessage = "O E-mail deve ser válido!")]
    public string Email { get; set; }
}