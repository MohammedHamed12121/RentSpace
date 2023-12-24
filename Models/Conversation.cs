using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentSpace.Models
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}