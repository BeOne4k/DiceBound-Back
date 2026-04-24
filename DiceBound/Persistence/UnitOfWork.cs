using DiceBound.Common;
using DiceBound.Entity_s.Characters;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Entity_s.Identity;
using DiceBound.Entity_s.Items;
using DiceBound.Interfaces;

namespace DiceBound.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DiceBoundDbContext _context;

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Character> Characters { get; }
        public IGenericRepository<Race> Races { get; }
        public IGenericRepository<Item> Items { get; }
        public IGenericRepository<Boss> Bosses { get; }
        public IGenericRepository<Mission> Missions { get; }
        public IGenericRepository<Enemy> Enemies { get; }

        public UnitOfWork(DiceBoundDbContext context)
        {
            _context = context;

            Users = new GenericRepository<User>(context);
            Characters = new GenericRepository<Character>(context);
            Races = new GenericRepository<Race>(context);
            Items = new GenericRepository<Item>(context);
            Bosses = new GenericRepository<Boss>(context);
            Missions = new GenericRepository<Mission>(context);
            Enemies = new GenericRepository<Enemy>(context);
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (typeof(T) == typeof(User)) return (IGenericRepository<T>)Users;
            if (typeof(T) == typeof(Character)) return (IGenericRepository<T>)Characters;
            if (typeof(T) == typeof(Race)) return (IGenericRepository<T>)Races;
            if (typeof(T) == typeof(Item)) return (IGenericRepository<T>)Items;
            if (typeof(T) == typeof(Boss)) return (IGenericRepository<T>)Bosses;
            if (typeof(T) == typeof(Mission)) return (IGenericRepository<T>)Missions;
            if (typeof(T) == typeof(Enemy)) return (IGenericRepository<T>)Enemies;


            throw new NotImplementedException($"No repository for {typeof(T).Name}");
        }
    }
}
