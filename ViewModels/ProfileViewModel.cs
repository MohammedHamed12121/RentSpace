using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Models;

namespace RentSpace.ViewModels
{
    public class ProfileViewModel
    {
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public List<Space> Space { get; set; }
    }
}