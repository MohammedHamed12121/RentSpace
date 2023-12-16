using RentSpace.Models;

namespace RentSpace.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<Space>> GetAllUserSpaces();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        // Entry<T> Entry<T>(T entity) where T : class;
        bool Update(AppUser user);
        bool Save();
    }
}