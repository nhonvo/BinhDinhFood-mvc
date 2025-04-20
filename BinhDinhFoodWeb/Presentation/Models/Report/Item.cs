using System.ComponentModel;
using BinhDinhFood.Domain.Entities;

namespace BinhDinhFood.Presentation.Models.Report;

public class Item
{
    public Product Product { get; set; }
    [DisplayName("Số lượng")]
    public int Quantity { get; set; }
    [DisplayName("Tổng tiền")]
    public double TotalCost => Product.ProductPrice * Quantity;
}
