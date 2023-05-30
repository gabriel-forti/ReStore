using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {   
        
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
            
        }

        // [HttpGet] // api/Products
        // public ActionResult<List<Product>> GetProducts() 
        // {
        //     var products = Context.Products.ToList();
        //     return Ok(products);
        // }

        [HttpGet] // api/Products
        public async Task<ActionResult<List<Product>>> GetProducts() 
        {
            //var products = await Context.Products.ToListAsync();
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")] // api/Products/{id}
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            //return await Ok(Context.Products.FindAsync(id));
            return await _context.Products.FindAsync(id);
        }
    }
}