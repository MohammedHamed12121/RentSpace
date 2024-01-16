using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Enums;

namespace RentSpace.Models
{
    public class Space
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        [MaxLength(250)]
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public DateTime? CreateAt { get; set; }
        
        public string? Contact { get; set; }
        public decimal InitialPrice {get; set;}

        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public SpaceCategoryEnum SpaceCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}