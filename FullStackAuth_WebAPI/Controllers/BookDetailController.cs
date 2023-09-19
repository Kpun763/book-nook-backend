using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
           
                

                //Retrieve all Books Reviews from the database, using Dtos
                var books = _context.Reviews.Select(b => new ReviewWithUserDTO
                {
                    Id = b.Id,
                    BookId = b.BookId,
                    Rating = b.Rating,
                    Text = b.Text,
                    
                    Username = new UserForDisplayDto
                    {
                        Id = b.User.Id,
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        UserName = b.User.UserName,
                    }
                }).ToList();


                double averageRating = 0.0;
                if (books.Any())
                {
                    averageRating = books.Average(r => r.Rating);
                }

                // Return the list of book reviews along with the average rating as a response
                var response = new
                {
                    Reviews = books,
                    AverageRating = averageRating
                };

                // Return the list of book reviews as a 200 OK response
                return StatusCode(200, books);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }


    }
}
