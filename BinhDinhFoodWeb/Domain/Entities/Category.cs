using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinhDinhFood.Domain.Entities;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(50)]
    [DisplayName("Tên loại")]
    public string CategoryName { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DisplayName("Ngày khởi tạo")]
    public DateTime CategoryDateCreated { get; set; } = DateTime.Now;
    public ICollection<Product> Products { get; set; }
}
