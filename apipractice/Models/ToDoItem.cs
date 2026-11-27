using System.ComponentModel.DataAnnotations;
namespace apipractice.Models
{
    public class TodoItem{
        public int Id{get; set;}

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title{get; set;} = string.Empty;

        public bool isComplete {get; set;}
    }
}