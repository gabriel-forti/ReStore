using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;

        public BasketController(StoreContext context)
        {
            _context = context;            
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(long productId, int quantity) 
        {
            // get basket || create basket
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();

            // get product
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return BadRequest(new ProblemDetails{Title = "Product Not Found"});

            // add item and save changes
            basket.AddItem(product, quantity);
            var result = await _context.SaveChangesAsync() > 0;

            // return resutl
            //if (result) StatusCode(201);
            if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }       

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(long productId, int quantity) 
        {
            // get basket
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();

            // remove item and save changes
            basket.RemoveItem(productId, quantity);
            var result = await _context.SaveChangesAsync() > 0;

            // return result
            if (result) return Ok();

            return BadRequest(new ProblemDetails{Title = "Problem removing item from basket"});
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(lb => lb.BuyerId == Request.Cookies["buyerId"]);
        }

        private Basket CreateBasket()
        {
            // creates cookie!!
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);

            // creates new basket
            var basket = new Basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);

            return basket;
        }

        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity

                }).ToList()
            };
        }
    }
}