using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<FavoritesController>
        [HttpGet, Authorize]
        public IActionResult Get()
        {
            try
            {
                string userId = User.FindFirstValue("id");
                //Retrieve all books from the database, using Dtos
                var favorites = _context.Favorites.Where(f => f.UserId == userId).ToList();

                // Return favorites as a 200 OK response
                return StatusCode(200, favorites);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }


      

        // POST api/<FavoritesController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Favorites data)
        {
            try
            {
                // Retrieve the authenticated user's ID from the JWT token
                string userId = User.FindFirstValue("id");

                // If the user ID is null or empty, the user is not authenticated, so return a 401 unauthorized response
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

               

                // Add the book to the database and save changes
                _context.Favorites.Add(data);
                if (!ModelState.IsValid)
                {
                    // If the favorite book state is invalid, return a 400 bad request response with the model state errors
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                // Return the newly created favorite book as a 201 created response
                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }


        // DELETE api/<FavoritesController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                // Find the Favorite to be deleted
                Favorites favorites = _context.Favorites.FirstOrDefault(c => c.Id == id);
                if (favorites == null)
                {
                    // Return a 404 Not Found error if the Favorite with the specified ID does not exist
                    return NotFound();
                }

                // Remove the favorite book from the database
                _context.Favorites.Remove(favorites);
                _context.SaveChanges();

                // Return a 204 No Content status code
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error with the error message if an exception occurs
                return StatusCode(500, ex.Message);
            }
        }
    }
}
