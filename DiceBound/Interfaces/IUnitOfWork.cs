using DiceBound.Common;
using DiceBound.Entity_s.Characters;
using DiceBound.Entity_s.Identity;
using DiceBound.Entity_s.Items;

namespace DiceBound.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Character> Characters { get; }
        IGenericRepository<Race> Races { get; }
        IGenericRepository<Item> Items { get; }

        Task<int> SaveAsync();

        IGenericRepository<T> Repository<T>() where T : BaseEntity;
    }
}
