
using System.ComponentModel.DataAnnotations;

namespace Accounts.Models
{
    public class NewAccount
    {
        [Required]
        [RegularExpression(@"\d+")]
        public string AccountNumber { get; set; } = default!;

        [RegularExpression(@"\d+(\.\d{0,2})?")]
        public decimal Amount { get; set; }

        [EnumDataType(typeof(PaymentMethod))]
        public int PaymentMethod { get; set; }
    }
}
