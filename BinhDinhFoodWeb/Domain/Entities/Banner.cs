using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinhDinhFood.Domain.Entities;

public class Banner
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BannerId { get; set; }
    [StringLength(50)]
    [DisplayName("Tên")]
    public string? BannerName { get; set; }
    [DisplayName("Khuyến mãi")]
    [Range(0, 1)]
    public int? ProductDiscount { get; set; }
    [DisplayName("Giá")]
    public double? BannerPrice { get; set; }
    [StringLength(200)]
    [DisplayName("Nội dung")]
    public string? BannerDescription { get; set; }
    [StringLength(50)]
    [DisplayName("Hình ảnh")]
    public string? BannerImage { get; set; }
    [DisplayName("Ngày thêm")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BannerDateCreated { get; set; } = DateTime.Now;
}
