using RentSpace.Models;

namespace RentSpace.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<Space>> GetAllUserSpaces(string id);
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}