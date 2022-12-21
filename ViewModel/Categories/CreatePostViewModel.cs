using System.ComponentModel.DataAnnotations;
using Blog.Models;

namespace Blog.ViewModel.Categories;

public class CreatePostViewModel
{
    
    [Required(ErrorMessage = "O titulo é obrigatório")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "O Summary é obrigatório")]
    public string Summary { get; set; }
    public string Body { get; set; }
    public string Slug { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Category Category { get; set; }
    public User Author { get; set; }

    public List<Tag> Tags { get; set; }
}