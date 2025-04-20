using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinhDinhFood.Domain.Entities;

public class Favorite
{

    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]

    [DisplayName("Mã sản phẩm")]
    public int ProductId { get; set; }
    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DisplayName("Mã khách hàng")]
    public int CustomerId { get; set; }
    [DisplayName("Ngày thêm")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime PRDateCreated { get; set; } = DateTime.Now;
    public Product? Product { get; set; }
    public Customer? Customer { get; set; }
}
