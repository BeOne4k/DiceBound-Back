using DiceBound.Common;
using DiceBound.Entities.Enums;

namespace DiceBound.Entity_s.Identity
{
    public class Subscription : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Inactive;
    }

}
