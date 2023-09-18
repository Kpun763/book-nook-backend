using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Text { get; set; }
        [Required]
        public double Rating { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

      
    }
}
