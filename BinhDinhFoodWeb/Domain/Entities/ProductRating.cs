using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinhDinhFood.Domain.Entities;

public class ProductRating
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductRatingId { get; set; }
    [Required]
    [DisplayName("Số sao")]
    public int Stars { get; set; }
    [DisplayName("Nội dung")]
    [StringLength(200)]
    public string? RatingContent { get; set; }
    [DisplayName("Ngày đánh giá")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime PRDateCreated { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public Product Product { get; set; }
    public Customer Customer { get; set; }
}
