using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentSpace.ViewModels
{
    public class EditUserProfileViewModel
    {
        public string? Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}