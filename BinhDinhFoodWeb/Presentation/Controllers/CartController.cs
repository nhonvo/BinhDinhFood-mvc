using System.Globalization;
using System.Security.Claims;
using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Presentation.Models.Authentication;
using BinhDinhFood.Presentation.Models.Payment;
using BinhDinhFood.Presentation.Models.Report;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BinhDinhFood.Controllers;

public class CartController : Controller
{
    readonly CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderDetailRepository _orderDetailRepo;
    private readonly IConfiguration _configuration;
    private readonly IFavoriteRepository _repoFavorite;

    private readonly double shippingCost = 30000;
    public CartController(ICartRepository cartRepo, IProductRepository productRepo, IOrderDetailRepository orderDetailRepo, IOrderRepository orderRepo, IUserRepository userRepository, IConfiguration configuration, IFavoriteRepository favoriteRepository)
    {
        _cartRepo = cartRepo;
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _orderDetailRepo = orderDetailRepo;
        _userRepository = userRepository;
        _repoFavorite = favoriteRepository;
        _configuration = configuration;
    }
    private void VnPayPayment(double totalMoney, int orderId)
    {
        string vnp_Returnurl = _configuration["VnPaySettings:vnp_Returnurl"]; //URL nhan ket qua tra ve 
        string vnp_Url = _configuration["VnPaySettings:vnp_Url"]; //URL thanh toan cua VNPAY 
        string vnp_TmnCode = _configuration["VnPaySettings:vnp_TmnCode"]; //Ma website
        string vnp_HashSecret = _configuration["VnPaySettings:vnp_HashSecret"]; //Chuoi bi mat

        //Get payment input

        OrderInfo order = new OrderInfo();
        order.OrderId = orderId;// Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
        order.Amount = (long)(totalMoney + shippingCost);// Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY giá gói
        order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
        order.OrderDesc = "Thanh toan Binh Dinh Food";
        order.CreatedDate = DateTime.Now;
        string locale = "vn";

        //Build URL for VNPAY
        VnPayLibrary vnpay = new VnPayLibrary();
        vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
        vnpay.AddRequestData("vnp_Command", "pay");
        vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
        vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
        var cboBankCode = "";
        if (cboBankCode != null && !string.IsNullOrEmpty(cboBankCode))
        {
            vnpay.AddRequestData("vnp_BankCode", cboBankCode);
        }
        vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
        vnpay.AddRequestData("vnp_CurrCode", "VND");
        vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(HttpContext.Request));
        if (!string.IsNullOrEmpty(locale))
        {
            vnpay.AddRequestData("vnp_Locale", locale);
        }
        else
        {
            vnpay.AddRequestData("vnp_Locale", "vn");
        }
        var orderCategory = "Thanh toán trực tuyến";
        vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
        vnpay.AddRequestData("vnp_OrderType", orderCategory); //default value: other
        vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
        vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
                                                                      //Add Params of 2.1.0 Version


        string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
        log.InfoFormat("VNPAY URL: {0}", paymentUrl);
        Response.Redirect(paymentUrl);
    }
    public async Task<IActionResult> VnPayResult()
    {
        if (HttpContext.Request.Query.Count > 0)
        {
            string vnp_HashSecret = _configuration["VnPaySettings:vnp_HashSecret"]; //Chuoi bi mat
            var vnpayData = HttpContext.Request.Query;
            VnPayLibrary vnpay = new VnPayLibrary();

            foreach (var ss in vnpayData)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(ss.Key) && ss.Key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(ss.Key, ss.Value);
                }
            }
            //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
            //vnp_TransactionNo: Ma GD tai he thong VNPAY
            //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
            //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

            //long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            //long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = HttpContext.Request.Query["vnp_SecureHash"];
            string TerminalID = HttpContext.Request.Query["vnp_TmnCode"];
            //long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            //String bankCode = HttpContext.Request.Query["vnp_BankCode"];
            string vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            string findId = new string(vnp_OrderInfo.Where(char.IsDigit).ToArray());
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    //await CompletePayment(true);
                    //return RedirectToAction("Index", "Home");
                    int orderId = Convert.ToInt32(findId);
                    await _orderRepo.UpdatePaymentState(orderId);
                    return RedirectToAction("Index", "Home");
                    //return View();
                }
            }
        }
        return RedirectToAction("Order", "Cart", new { orderFailed = true });
    }
    public void MOMOPayment(double totalMoney, int orderID)
    {
        string endpoint = _configuration["MOMOSettings:endpoint"];
        string partnerCode = _configuration["MOMOSettings:partnerCode"];
        string accessKey = _configuration["MOMOSettings:accessKey"];
        string serectkey = _configuration["MOMOSettings:serectkey"];
        string orderInfo = "thanh_toan";
        string redirectUrl = _configuration["MOMOSettings:returnMOMO"];
        string ipnUrl = _configuration["MOMOSettings:returnMOMO"];
        string requestType = "captureWallet";

        string amount = Convert.ToInt32(totalMoney).ToString();
        string orderId = orderID.ToString();
        string requestId = Guid.NewGuid().ToString();
        string extraData = "";
        string rawHash = "accessKey=" + accessKey +
            "&amount=" + amount +
            "&extraData=" + extraData +
            "&ipnUrl=" + ipnUrl +
            "&orderId=" + orderId +
            "&orderInfo=" + orderInfo +
            "&partnerCode=" + partnerCode +
            "&redirectUrl=" + redirectUrl +
            "&requestId=" + requestId +
            "&requestType=" + requestType
            ;
        log.Debug("rawHash = " + rawHash);

        MoMoSecurity crypto = new MoMoSecurity();
        //sign signature SHA256
        string signature = crypto.signSHA256(rawHash, serectkey);
        log.Debug("Signature = " + signature);
        //build body json request
        JObject message = new JObject
        {
            { "partnerCode", partnerCode },
            { "partnerName", "Test" },
            { "storeId", "MomoTestStore" },
            { "requestId", requestId },
            { "amount", amount },
            { "orderId", orderId },
            { "orderInfo", orderInfo },
            { "redirectUrl", redirectUrl },
            { "ipnUrl", ipnUrl },
            { "lang", "en" },
            { "extraData", extraData },
            { "requestType", requestType },
            { "signature", signature }

        };
        log.Debug("Json request to MoMo: " + message.ToString());
        string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
        ViewBag.endpoint = responseFromMomo;
        JObject jmessage = JObject.Parse(responseFromMomo);
        //  ViewBag.hi = responseFromMomo;
        log.Debug("Return from MoMo: " + jmessage.ToString());
        System.Diagnostics.Process.Start(jmessage.GetValue("payUrl").ToString());
    }
    public async Task<IActionResult> MOMOResult()
    {
        if (HttpContext.Request.Query.Count > 0)
        {
            string orderID = HttpContext.Request.Query["orderId"];
            string findId = new string(orderID.Where(char.IsDigit).ToArray());
            int orderId = Convert.ToInt32(findId);
            await _orderRepo.UpdatePaymentState(orderId);
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Index()
    {
        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        ViewData["TotalSubMoney"] = TotalMoney().ToString("#,###", cul.NumberFormat);
        ViewData["ShippingCost"] = shippingCost.ToString("#,###", cul.NumberFormat);
        ViewData["TotalMoney"] = (shippingCost + TotalMoney()).ToString("#,###", cul.NumberFormat);

        return View(listCart);
    }
    public IActionResult UpdateCart()
    {
        List<Item> cart = _cartRepo.Get(HttpContext.Session);
        ViewData["TotalSubMoney"] = TotalMoney().ToString("#,###", cul.NumberFormat); ;
        ViewData["ShippingCost"] = shippingCost.ToString("#,###", cul.NumberFormat); ;
        ViewData["TotalMoney"] = (shippingCost + TotalMoney()).ToString("#,###", cul.NumberFormat); ;
        return ViewComponent("CartComponent", cart);
    }
    // sum all money 
    public IActionResult UpdateItem(int id, int value)
    {
        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        Item cart = listCart.FirstOrDefault(x => x.Product.ProductId == id);
        if (cart != null)
        {
            var quantity = cart.Quantity + value;
            if (quantity >= 0)
            {
                cart.Quantity = quantity;
            }

            _cartRepo.Set(HttpContext.Session, listCart);
        }
        return ViewComponent("ItemComponent", cart);
    }
    public double TotalMoney()
    {
        double totalMoney = 0;
        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        if (listCart != null)
        {
            totalMoney = listCart.Sum(x => x.Quantity * (x.Product.ProductPrice - x.Product.ProductPrice * x.Product.ProductDiscount / 100));
        }
        return totalMoney;
    }
    // Add product to cart
    public async Task<IActionResult> AddInCart(int id, int? quantity)
    {

        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        bool isInCart = listCart.Any(x => x.Product.ProductId == id);
        if (quantity == null)
        {
            quantity = 1;
        }

        if (!isInCart)
        {
            Item newItem = new Item { Product = await _productRepo.GetByIdAsync(id), Quantity = (int)quantity };
            listCart.Add(newItem);
        }
        else
        {
            Item? item = listCart.FirstOrDefault(x => x.Product.ProductId == id);
            item.Quantity += (int)quantity;
        }
        _cartRepo.Set(HttpContext.Session, listCart);
        return ViewComponent("MiniCartComponent");
    }
    // remove product in cart
    public IActionResult RemoveInCart(int id)
    {

        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        bool cart = listCart.Any(x => x.Product.ProductId == id);
        if (cart)
        {
            listCart.RemoveAll(x => x.Product.ProductId == id);
            _cartRepo.Set(HttpContext.Session, listCart);
        }

        return ViewComponent("CartComponent", listCart);
    }
    public IActionResult RemoveInMiniCart(int id)
    {
        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        bool cart = listCart.Any(x => x.Product.ProductId == id);
        if (cart)
        {
            listCart.RemoveAll(x => x.Product.ProductId == id);
            _cartRepo.Set(HttpContext.Session, listCart);
        }

        return ViewComponent("MiniCartComponent");
    }

    // remove all product in cart
    public IActionResult RemoveAll()
    {
        _cartRepo.Set(HttpContext.Session, new List<Item>());
        return ViewComponent("CartComponent", new List<Item>());
    }
    // Order - checkout
    public IActionResult Order(bool orderFailed = false)
    {
        // Authentication user
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login", "User");
        }

        if (orderFailed)
        {
            ViewBag.ErrorMessage = "Thanh toán lỗi";
        }

        List<Item> listCart = _cartRepo.Get(HttpContext.Session);
        if (listCart.Count() == 0)
        {
            RedirectToAction("Index", "Cart");
        }
        // get customer 
        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        InfoViewModel customer = _userRepository.GetUserInfo(id);
        ViewBag.Name = customer.FullName;
        ViewBag.Phone = customer.Phone;
        ViewBag.Address = customer.Address;
        ViewBag.Email = customer.Email;



        ViewData["TotalSubMoney"] = TotalMoney().ToString("#,###", cul.NumberFormat);
        ViewData["ShippingCost"] = shippingCost.ToString("#,###", cul.NumberFormat);
        ViewData["TotalMoney"] = (shippingCost + TotalMoney()).ToString("#,###", cul.NumberFormat);

        return View(listCart);
    }
    [HttpPost]
    public async Task Order(IFormCollection form)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Customer"))
        {

            var totalMoney = TotalMoney() + shippingCost;

            List<Item> listCart = _cartRepo.Get(HttpContext.Session);

            int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Order or = new Order();
            or.CustomerId = id;
            or.DayOrder = DateTime.Now;
            or.DayDelivery = DateTime.Now.AddDays(3);
            or.PaidState = false;
            // build payment momo or vnpay
            or.DeliveryState = false;
            or.TotalMoney = totalMoney;
            await _orderRepo.AddAsync(or);
            await _orderRepo.SaveAsync();

            foreach (var item in listCart)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderId = or.OrderId;
                detail.ProductId = item.Product.ProductId;
                detail.Quantity = item.Quantity;
                detail.UnitPrice = Convert.ToDecimal(item.Product.ProductPrice);
                await _productRepo.UpdateAmount(item.Product.ProductId);
                await _orderDetailRepo.AddAsync(detail);
                await _orderDetailRepo.SaveAsync();
            }
            // clear cart
            _cartRepo.Set(HttpContext.Session, new List<Item>());
            int paymentMethod = Convert.ToInt32(form["ChoosePaymentMethod"]);
            if (paymentMethod == 2)
            {
                VnPayPayment(totalMoney, or.OrderId);
            }
            else if (paymentMethod == 1)
            {
                MOMOPayment(totalMoney, or.OrderId);
            }
            else if (paymentMethod == 0)
            {
                Response.Redirect("Confirm");
            }
        }

    }
    public IActionResult Checkout()
    {

        return View();
    }
    // Comfirm order
    public IActionResult Confirm()
    {
        return View();
    }
    // Your order
    public async Task<IActionResult> TrackOrderAsync()
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login", "User");
        }

        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        IEnumerable<Order> obj = await _orderRepo.GetListAsync(filter: x => x.CustomerId == id);
        return View(obj);
    }
    // Your order detail
    public async Task<IActionResult> OderDetail(int id)
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login", "User");
        }

        IEnumerable<OrderDetail> obj = await _orderDetailRepo.GetListAsync(filter: x => x.OrderId == id, includeProperties: "Product");
        return View(obj);
    }
    // get favorite List
    public async Task<IActionResult> FavoriteList()
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login", "User");
        }

        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var obj = await _repoFavorite.GetListAsync(filter: x => x.CustomerId == id, includeProperties: "Product");
        return View(obj);
    }
    // add in favorite List
    public async Task AddInFavorite(int id)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Customer"))
        {
            int userid = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var listFav = await _repoFavorite.GetListAsync(filter: x => x.CustomerId == userid);

            bool isInFav = listFav.Any(x => x.ProductId == id);
            if (!isInFav)
            {
                Favorite newItem = new Favorite
                {
                    Product = await _productRepo.GetByIdAsync(id),
                    CustomerId = userid
                };
                await _repoFavorite.AddAsync(newItem);
                await _repoFavorite.SaveAsync();
            }
        }
    }
    // remove in favorite list
    public async Task<IActionResult> RemoveInFavorite(int id)
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login", "User");
        }

        int userid = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var listFav = await _repoFavorite.GetListAsync(filter: x => x.CustomerId == userid);

        bool isInFav = listFav.Any(x => x.ProductId == id);
        if (isInFav)
        {
            _repoFavorite.Delete(where: x => x.ProductId == id);
        }
        await _repoFavorite.SaveAsync();
        var Obj = await _repoFavorite.GetListAsync(filter: x => x.CustomerId == userid);
        return RedirectToAction("FavoriteList");
        //return ViewComponent("FavComponent", Obj);
    }
    // compare with other products 
    public IActionResult Compare(int id)
    {
        return View();
    }
}
