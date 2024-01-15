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
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: implement add for admin dashboard
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        // TODO: implement delete for admin dashboard
        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetAllFavoriteAsync(string userId)
        {
            return await _context.Users
                                .Where(u => u.Id == userId)
                                .FirstOrDefaultAsync();
                                // .Include(u => u.Favorite);
                                
                                // .FirstOrDefaultAsync()
                                // .Include(u => u.Favorite)
                                // .ThenInclude(f => f.Post)
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdAsyncWithNoTracking(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id ==id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}