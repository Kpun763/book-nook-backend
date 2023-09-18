using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public string Text { get; set; }
        [Required]
        public double Rating { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string User { get; set; }

      
    }
}
