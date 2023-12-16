using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Models;

namespace RentSpace.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> GetUserByIdAsyncWithNoTracking(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}