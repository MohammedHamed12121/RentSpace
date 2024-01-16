using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentSpace.Models
{
    [PrimaryKey(nameof(UserId), nameof(SpaceId))]
    public class Favorite
    {
        public string? Id { get; set; }
        public int SpaceId { get; set; }
        public Space? Space { get; set; }
        public string? UserId { get; set; }

        public virtual AppUser? User { get; set; }
    }
}