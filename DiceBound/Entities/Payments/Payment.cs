using DiceBound.Common;
using DiceBound.Entity_s.Identity;

namespace DiceBound.Entity_s.Payments
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
    }

}
