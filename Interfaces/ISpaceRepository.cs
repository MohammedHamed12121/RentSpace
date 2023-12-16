using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Models;

namespace RentSpace.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAllSpaceAsync();
        Task<Space?> GetSpaceByIdAsync(int id);
        Task<Space?> GetSpaceByIdAsyncWithNoTracking(int id);
        Task<IEnumerable<Space>> GetSpaceByCityAsync(string city);
        Task<string> GetSpaceUserNameAsync(string id);
        bool Add(Space space);

        bool Update(Space space);

        bool Delete(Space space);

        bool Save();
    }
}