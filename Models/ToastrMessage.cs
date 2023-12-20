using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentSpace.Enums;

namespace RentSpace.Models
{
    public class ToastrMessage
    {
        public string? Message { get; set; }
        public ToastrMessageType Type { get; set; }
        // public string? Title { get; set; }
    }
}