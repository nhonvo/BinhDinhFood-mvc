using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Presentation.Models;
using BinhDinhFood.Presentation.Models.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmStatisticsController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICategoryRepository _categoryRepository;
    private static int xx = 0, yy = 0, zz = 0;
    public AdmStatisticsController(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _orderDetailRepository = orderDetailRepository;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _categoryRepository = categoryRepository;
    }
    public IActionResult Index()
    {
        return View();
    }
    private string GetRandomColour()
    {
        Random random = new Random();
        xx = (xx + DateTime.Now.Millisecond + DateTime.Now.Second + random.Next(24)) % 256;
        yy = (yy + DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Hour) % 256;
        zz = (zz + DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute) % 256;
        return "rgb(" + xx.ToString() + "," + yy.ToString() + "," + zz.ToString() + ")";
    }
    //Lấy ra danh sách Customer : Số đơn hàng
    public IActionResult StaticOrdersByUsers(int option = 0)
    {
        Period time = new Period(option);
        return View(_customerRepository.GetTopOrder(time.startDate, time.endDate).OrderByDescending(x => x.Value));
    }
    //Lấy ra danh sách Customer : Tổng số tiền đã chi trong khoảng thời gian gần đây
    public IActionResult StaticRevenueByUsers(int option = 0)
    {
        Period time = new Period(option);
        return View(_customerRepository.GetTopRevenue(time.startDate, time.endDate).OrderByDescending(x => x.Value));
    }
    //Trả về khoảng thời gian tương ứng với option
    private string TimeOption(int option)
    {
        switch (option)
        {
            case 1:
                return "tháng này";
            case 2:
                return "năm nay";
            case 3:
                return "từ trước đến nay";
            default:
                return "hôm nay";
        }
    }
    //Truyền dữ liệu cho bar chart
    //bar là data set up cho chart, data là data đã được lấy ra từ cơ sở dữ liệu
    private void SetupBarChart(Bar bar, Table[] data)
    {
        int amount = data.Count();// số lượng cột của chart

        for (int i = 0; i < amount; i++)
        {
            bar.data.labels[i] = data[i].Key;//tên các cột 
            bar.data.datasets[0].backgroundColor[i] = GetRandomColour();//màu ngẫu nhiên cho cột
            bar.data.datasets[0].borderColor[i] = GetRandomColour();//màu viền cho cột
            bar.data.datasets[0].data[i] = data[i].Value;//giá trị của cột
                                                         //  bar.data.datasets[0].label = data[i].Key;
        }
    }
    //Truyền dữ liệu cho line chart
    //line là data set up cho Line chart
    private void SetupLineChart(Line line, Table[] data)
    {
        for (int i = 1; i <= 12; i++)
        {
            line.data.labels[i - 1] = data[i].Key;
            line.data.datasets[0].borderColor = GetRandomColour();
            line.data.datasets[0].data[i - 1] = data[i].Value;
        }
    }
    private void SetupPieChart(Pie pie, Table[] data)
    {
        int amount = data.Count();
        for (int i = 0; i < amount; i++)
        {
            pie.data.labels[i] = data[i].Key;
            pie.data.datasets[0].backgroundColor[i] = GetRandomColour();
            pie.data.datasets[0].data[i] = data[i].Value;
        }
    }

    //Thống kê số lượng sản phẩm bán ra trong khoảng thời gian option bằng bar chart 
    //option default là trong ngày
    public IActionResult StaticProductAmount(int option = 0)
    {
        //set up giá trị default cho select list
        ViewData["option"] = option;
        ViewData["optionName"] = TimeOption(option);

        //Lấy khoảng thời gian tương ứng với option
        Period time = new Period(option);

        //Lấy data từ cơ sở dữ liệu
        var data = _productRepository
            .GetProductWithAmount(time.startDate, time.endDate)
            .OrderByDescending(x => x.Value).ToArray();
        int amount = data.Count();// số lượng cột của chart

        //Tạo chart mới với số cột amount
        Bar productAmount = new Bar(amount);//Setup các thông số cho chart
        SetupBarChart(productAmount, data);
        productAmount.options.plugins.title.text = "Số lượng sản phẩm bán được trong " + TimeOption(option);//Đặt tên cho chart
        //productAmount.data.labels[0] = "Doanh số";
        return View(productAmount);
    }
    //Thống kê doanh thu từng sản phẩm bán ra trong khoảng thời gian option bằng bar chart 
    //option default là trong ngày
    public IActionResult StaticProductRevenue(int option = 0)
    {
        //set up giá trị default cho select list
        ViewData["option"] = option;
        ViewData["optionName"] = TimeOption(option);

        //Lấy khoảng thời gian tương ứng với option
        Period time = new Period(option);

        //Lấy data từ cơ sở dữ liệu
        var data = _productRepository.GetProductWithRevenue(time.startDate, time.endDate).OrderByDescending(x => x.Value).ToArray();
        int amount = data.Count();// số lượng cột của chart

        //Tạo chart mới với số cột amount
        Bar productRevenue = new Bar(amount);
        SetupBarChart(productRevenue, data);//Setup các thông số cho chart
        productRevenue.options.plugins.title.text = "Doanh thu sản phẩm bán được trong " + TimeOption(option);// Đặt tên cho chart
                                                                                                              // productRevenue.data.labels[0] = "Doanh thu";
        return View(productRevenue);
    }
    //Thống kê số lượng bán được/ doanh thu của sản phẩm product theo tháng trong năm nay bằng line chart
    //option default là số lượng bán được
    public IActionResult StaticProduct(int product = 3, int option = 0)
    {
        string text = option == 0 ? "Doanh số" : "Doanh thu";
        ViewData["option"] = option;
        ViewData["optionName"] = text;
        var listProduct = _productRepository.GetListProduct();
        ViewData["listProduct"] = new SelectList(listProduct, "Value", "Key");
        Table[] data;
        switch (option)
        {
            case 1:
                data = _productRepository.GetProductRevenueInYear(product);
                break;
            default:
                data = _productRepository.GetProductAmountInYear(product);
                break;
        }
        Line productLine = new Line(12);
        productLine.data.datasets = new Line.Data.Dataset[1];
        productLine.data.datasets[0] = new Line.Data.Dataset(12);
        SetupLineChart(productLine, data);
        productLine.options.plugins.title.text = text;
        productLine.data.datasets[0].label = text;
        return View(productLine);

    }
    //Thống kê cơ cấu doanh thu theo năm {option} bằng pie chart
    //option default là năm nay
    public IActionResult RevenueStructure(int? option)
    {
        if (option == null)
        {
            option = DateTime.Now.Year;
        }

        ViewData["option"] = option;
        var data = _categoryRepository.GetRevenueStructure((int)option);
        int amount = data.Count();
        Pie revenueStructure = new Pie(amount);
        revenueStructure.options.plugins.title.text = "Cơ cấu doanh thu năm " + option;
        SetupPieChart(revenueStructure, data);
        return View(revenueStructure);

    }
}
