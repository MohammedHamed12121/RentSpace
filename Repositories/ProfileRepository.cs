using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentSpace.Data;
using RentSpace.Extensions;
using RentSpace.Interfaces;
using RentSpace.Models;

namespace RentSpace.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProfileRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Space>> GetAllUserSpaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userSpacess = _context.Spaces.Where(r => r.AppUserId == curUser);
            return await userSpacess.ToListAsync();
        }



        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        // public Entry<T> Entry<T>(T entity) where T : class
        // {
        //     return _context.Entry(entity);
        // }
    }
}