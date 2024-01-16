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

        public bool Add(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            return Save();
        }

        public bool Delete(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);
            return Save();
        }

        public bool FavoriteExists(string userId, int spaceId)
        {
            return _context.Favorites.Any(f => f.UserId == userId && f.SpaceId == spaceId);
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                            .Include(f => f.Space)
                            .Where(f => f.UserId == userId)
                            .ToListAsync();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}