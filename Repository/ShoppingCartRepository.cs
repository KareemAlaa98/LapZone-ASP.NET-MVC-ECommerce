﻿using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        ApplicationDbContext context;
        IProductRepository productRepository;
        public ShoppingCartRepository(ApplicationDbContext _context, IProductRepository _productRepository)
        {
            this.context = _context;
            this.productRepository = _productRepository;
        }

        public ShoppingCart GetUserShoppingCart(string userId)
        {
            return context.ShoppingCarts
                              .Include(e => e.ApplicationUser)
                              .Include(e => e.CartItems)
                                .ThenInclude(e => e.Product)
                                  .ThenInclude(p => p.ProductImages)
                              .Include(e => e.CartItems)
                                .ThenInclude(e => e.Product)
                                  .ThenInclude(p => p.Discount)
                              .FirstOrDefault(e => e.ApplicationUserId == userId);
        }

        public void AddProductToCart(string userId, int productId) 
        {
            var product = productRepository.ReadById(productId);
            var userShoppingCart = GetUserShoppingCart(userId);

            foreach(var item in userShoppingCart.CartItems)
            {
                if(item.ProductId == productId)
                {
                    return;
                }
            }

            decimal newPrice = 0;
            if(product.DiscountId != null) 
            {
                newPrice = product.Price - (product.Price * product.Discount.DiscountPercent/100);
            }
            else { newPrice = product.Price; }

            CartItem cartItem = new CartItem();
            cartItem.ShoppingCartId = userShoppingCart.Id;
            cartItem.ProductId = productId;
            cartItem.Quantity = 1;

            context.CartItems.Add(cartItem);
            context.SaveChanges();

            userShoppingCart.Total += cartItem.Quantity * newPrice;
            context.SaveChanges();
        }

    }
}