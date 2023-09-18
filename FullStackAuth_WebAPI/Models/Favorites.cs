using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    public class Favorites
    {
        [Key] 
        public int Id { get; set; }

        [Required]

        public string BookId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public string User { get; set; }

    }
}
