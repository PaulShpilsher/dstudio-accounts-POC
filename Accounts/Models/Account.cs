

namespace Accounts.Models
{
    public class Account
    {
        public string AccountNumber { get; set; } = default!;
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public int PaymentMethod { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
