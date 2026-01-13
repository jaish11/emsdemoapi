using System.ComponentModel.DataAnnotations;

namespace EmsDemoBlazer.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Book name is required")]
        public string Name { get; set; }
    }
}
