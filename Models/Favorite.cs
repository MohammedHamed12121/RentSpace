using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentSpace.Models
{
    public class Favorite
    {
        public string? Id { get; set; }
        public int SpaceId { get; set; }
        public Space? Space { get; set; }
    }
}