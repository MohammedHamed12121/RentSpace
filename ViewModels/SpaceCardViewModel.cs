using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Models;

namespace RentSpace.ViewModels
{
    public class SpaceCardViewModel
    {
        public Space space { get; set; }
        public string PostUserName { get; set; }
    }
}