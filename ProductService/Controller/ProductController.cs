using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if the product with the specified ID is not found
            }

            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                _context.Products.Add(product); // Add the new product to the context
                _context.SaveChanges(); // Save changes to the database

                return StatusCode(StatusCodes.Status201Created,product); // Return HTTP status 200 (OK) indicating success
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the product."); // Return HTTP status 500 (Internal Server Error) with an error message
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            var existingProduct = _context.Products.Find(id);

            if (existingProduct == null)
            {
                return NotFound(); // Return 404 Not Found if the product with the specified ID is not found
            }

            existingProduct.Name = product.Name; // Update the product name

            _context.SaveChanges(); // Save changes to the database

            return NoContent(); // Return HTTP status 204 (No Content) indicating success
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if the product with the specified ID is not found
            }

            _context.Products.Remove(product); // Remove the product from the context
            _context.SaveChanges(); // Save changes to the database

            return NoContent(); // Return HTTP status 204 (No Content) indicating success
        }
    }
}
