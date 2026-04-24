using DiceBound.Common;
using DiceBound.Entities.Enums;
using DiceBound.Entity_s.Characters;
using DiceBound.Entity_s.Payments;

namespace DiceBound.Entity_s.Identity
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;


        public Subscription? Subscription { get; set; }

        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

}
