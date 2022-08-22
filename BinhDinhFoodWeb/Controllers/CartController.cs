﻿using BinhDinhFood.Models;
using BinhDinhFoodWeb.Intefaces;
using BinhDinhFoodWeb.Models;
using BinhDinhFoodWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BinhDinhFoodWeb.Controllers
{
    public class CartController : Controller
    {
        // declare _repo but don't use
        private readonly ICartRepository _repo;
        private readonly IProductRepository _pdRepo;
        public static List<Cart> listCartStatic = new List<Cart>();
        public CartController(ICartRepository repo, IProductRepository pdRepo)
        {
            _repo = repo;
            _pdRepo = pdRepo;
        }
        public IActionResult Index()
        {
            SetAll();
            List<Cart> listCart = GetAll();
            ViewData["TotalSubMoney"] = TotalMoney();
            ViewData["TotalMoney"] = 30000 + TotalMoney();

            Cart obj = new Cart() { dUnitPrice = 500, iProductId = 2, iQuantity = 2, sProductImage = "goica.png", sProductName = "nhon" };
            Cart obj1 = new Cart() { dUnitPrice = 1000, iProductId = 1, iQuantity = 2, sProductImage = "goica.png", sProductName = "nhondeptai" };
            listCart.Add(obj);
            listCart.Add(obj1);

            return View(listCart);
        }
        // get all product in cart
        public List<Cart> SetAll()
        {
            //Get data cart from session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(listCartStatic));
            return listCartStatic;
        }
        public List<Cart> GetAll()
        {
            //Get data cart from session
            List<Cart> listCart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("Cart"));
            
            return listCart != null ? listCart : new List<Cart>();
        }
        
        // sum all money 
        public double TotalMoney()
        {
            double totalMoney = 0;
            List<Cart> listCart = GetAll();
            if (listCart != null)
            {
                totalMoney = (double)listCart.Sum(x => x.dTotalMoney);
            }
            return totalMoney;
        }
        // Add product to cart
        public async Task<IActionResult> AddInCart(int id)
        {
            List<Cart> listCart = GetAll();
            Cart cart = listCart.FirstOrDefault(x => x.iProductId == id);
            if(cart == null)
            {
                Product pd = await _pdRepo.GetProducts(id);
                cart = new Cart(pd);
                listCart.Add(cart);
                SetAll();
            }
            else
            {
                cart.iQuantity++;
            }

            GetAll();
            return RedirectToAction("Index", "Home");
        }
        // remove product in cart
        public IActionResult RemoveInCart(int id) 
        {
            List<Cart> listCart = GetAll();
            Cart cart = listCart.SingleOrDefault(x => x.iProductId == id);
            if(cart != null)
            {
                listCart.RemoveAll(x=> x.iProductId ==id);
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
        // remove all product in cart
        public IActionResult RemoveAll()
        {
            List<Cart> listCart = GetAll();
            listCart.Clear();
            return RedirectToAction("Cart");
        }
        // Update Cart
            // ? update in form
        public IActionResult UpdateCart()
        {
            return View();
        }
        
        // Order - checkout
        public IActionResult Checkout()
        {
            return View();
        }
        // Comfirm order
        public IActionResult Comfirm()
        {
            return View();
        }
        // Track your order
        public IActionResult TrackOder()
        {
            return View();
        }
        // Partial View
        // list product
        // total product or viewbag and transmissive to another controller
        public IActionResult ListProductPartial()
        {
            List<Cart> listCart = GetAll();

            return PartialView(listCart);
        }
        // total product -- didn't apply view
        public IActionResult TotalCartPartial()
        {
            List<Cart> listCart = GetAll();
            var total = listCart.Count();
            return PartialView(total);
        }
    }
}
