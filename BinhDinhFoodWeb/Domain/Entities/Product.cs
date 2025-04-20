using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BinhDinhFood.Domain.Entities;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Mã sản phẩm")]
    public int ProductId { get; set; }
    [Required]
    [StringLength(100)]
    [DisplayName("Tên sản phẩm")]
    public string ProductName { get; set; }
    [DisplayName("Giá")]
    public double ProductPrice { get; set; }
    [DisplayName("Nội dung")]
    public string? ProductDescription { get; set; }
    [DisplayName("Số lượng")]
    public int ProductAmount { get; set; }
    [DisplayName("Khuyến mãi")]
    [Range(0, 1)]
    public int ProductDiscount { get; set; }
    [DisplayName("Đánh giá")]
    public int ProductRating { get; set; }
    [DisplayName("Hình ảnh")]
    public string? ProductImage { get; set; }
    [DisplayName("Ngày thêm")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

    public DateTime ProductDateCreated { get; set; } = DateTime.Now;
    [DisplayName("Loại sản phẩm")]
    [JsonIgnore]
    [IgnoreDataMember]
    public virtual Category? Category { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    public virtual ICollection<ProductRating>? ProductRatings { get; set; }
    public virtual ICollection<Favorite>? Favorites { get; set; }
}
