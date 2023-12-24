using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RentSpace.Models;

namespace RentSpace.ViewModels
{
    public class ChatViewModel
    {
        public IdentityUser? User { get; set; }
        public IEnumerable<Conversation>? Messages { get; set; }
        public string? Reciever { get; set; }
        public string? Message { get; set; }
    }
}