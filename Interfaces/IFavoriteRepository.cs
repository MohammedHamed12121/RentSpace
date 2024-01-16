using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Models;

namespace RentSpace.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetUserFavoritesAsync(string userId);
        bool Add(Favorite favorite);
        bool FavoriteExists(string userId, int spaceId);
        bool Delete(Favorite favorite);
        bool Save();
    }
}