﻿using Microsoft.AspNetCore.Mvc;
using PetShop.Data;
using System.Linq;

namespace BooksStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private MyCart cart;
        public OrderController(IOrderRepository repoService, MyCart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Trong giỏ hàng rỗng");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }
        }
    }
}
   