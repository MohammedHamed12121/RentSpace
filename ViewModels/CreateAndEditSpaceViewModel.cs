using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Enums;

namespace RentSpace.ViewModels
{
    public class CreateAndEditSpaceViewModel
    {
        public int Id { get; set; }
        public string? SpaceUserName { get; set; }
        public string? Title { get; set; }
        [MaxLength(250)]
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public SpaceCategoryEnum SpaceCategory { get; set; }
        public string? AppUserId { get; set; }
    }
}