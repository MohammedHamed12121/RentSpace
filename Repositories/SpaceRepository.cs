using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentSpace.Data;
using RentSpace.Interfaces;
using RentSpace.Models;

namespace RentSpace.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ApplicationDbContext _context;
        public SpaceRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public bool Add(Space space)
        {
            _context.Add(space);
            return Save();
        }

        public bool Delete(Space space)
        {
            _context.Remove(space);
            return Save();
        }

        public async Task<IEnumerable<Space>> GetAllSpaceAsync()
        {
            return await _context.Spaces.ToListAsync();
        }

        public async Task<IEnumerable<Space>> GetSpaceByCityAsync(string city)
        {
            return await _context.Spaces.Where(r => r.City == city).ToListAsync();
        }

        public async Task<Space?> GetSpaceByIdAsync(int id)
        {
            return await _context.Spaces.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Space?> GetSpaceByIdAsyncWithNoTracking(int id)
        {
            return await _context.Spaces.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string> GetSpaceUserNameAsync(string id)
        {
            var space =  await _context.Spaces.Include(s => s.AppUser).FirstOrDefaultAsync(s => s.AppUserId == id);
            return space.AppUser.UserName.ToString();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Space space)
        {
            _context.Update(space);
            return Save();
        }
    }
}