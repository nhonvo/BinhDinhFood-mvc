using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinhDinhFood.Domain.Entities;

public class OrderDetail
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DisplayName("Mã đơn đặt hàng")]
    public int OrderId { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DisplayName("Mã sản phẩm")]
    public int ProductId { get; set; }

    [Column(TypeName = "money")]
    [DisplayName("Đơn giá")]
    public decimal UnitPrice { get; set; }
    [DisplayName("Số lượng")]

    public int Quantity { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }

}
