using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{    
    public class ProductsController : BaseApiController
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
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}