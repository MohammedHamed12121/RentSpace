using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Models;

namespace RentSpace.Interfaces
{
    public interface IFavoriteRepository
    {
        bool AddFavorite(Favorite favorite);
        bool DeleteFavorite(Favorite favorite);
        bool Save();
    }
}