using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/bookdetails")]
    [ApiController]
    public class BookDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<BookDetailController>/5
        [HttpGet("{bookId}"), Authorize]
        public IActionResult Get(string bookId)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var reviews = _context.Reviews.Include(r => r.User).Where(r => r.BookId == bookId).ToList();
         
                bool isFavorite = false;
                var favorites = _context.Favorites.Where(f => f.BookId == bookId && f.UserId == userId).ToList();
                if(favorites.Count > 0)
                {
                    isFavorite = true;
                }
                
                var response = new BookDetailsDto
                {
                    BookId = bookId,
                    Reviews =  reviews.Select(r => new ReviewWithUserDTO
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Text = r.Text,
                    
                    User = new UserForDisplayDto
                    {
                        Id = r.User.Id,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName,
                        UserName = r.User.UserName,
                    }
                }).ToList(),
                    AverageRating = reviews.Average(r => r.Rating),
                    isFavorite = isFavorite
                
                };
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }


    }
}
