using System.ComponentModel.DataAnnotations;

namespace apipractice.Models
{
    public class TodoPatchRequest
    {
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string? Title{get; set;}

        public bool? IsComplete {get; set;}
    }
}