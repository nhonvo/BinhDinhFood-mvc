using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinhDinhFood.Presentation.Models.Authentication;

public class Token
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int TokenID { get; set; }
    [Required]
    public string CustomerUserName { get; set; }
    [Required]
    public string TokenValue { get; set; }
    [Required]
    public DateTime Expiry { get; set; }
    public Token(string customerUserName, string tokenValue, DateTime expiry)
    {
        CustomerUserName = customerUserName;
        TokenValue = tokenValue;
        Expiry = expiry;
    }
}
