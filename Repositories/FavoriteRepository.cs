using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentSpace.Data;
using RentSpace.Interfaces;
using RentSpace.Models;

namespace RentSpace.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddFavorite(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            return Save();
        }

        public bool DeleteFavorite(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);
            return Save();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}