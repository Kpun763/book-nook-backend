using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<BookDetailController>/5
        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            try
            {
                var book = await _context.BookDetailsDtos.FindAsync(id);
              

                if (book == null)
                {
                    return NotFound();
                }

                var reviews = await _context.Reviews
                .Where(r => r.BookId == id)
                .Select(r => new ReviewWithUserDTO
                {
                    Id = r.BookId,
                    Text = r.Text,
                    Rating = r.Rating,
                })
                    .ToListAsync();
                double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                var bookDetails = new BookDetailsDto
                {
                    Reviews = reviews,
                    AverageRating = averageRating,

                };
                return Ok(bookDetails);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            
        }


    }
}
