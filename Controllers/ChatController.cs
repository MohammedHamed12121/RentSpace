using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RentSpace.Data;
using RentSpace.Extensions;
using RentSpace.Models;
using RentSpace.ViewModels;

namespace RentSpace.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(ILogger<ChatController> logger, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrivateChat(string id)
        {
            // get the reciever
            var reciever = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            // get the messages form the database between those two users
            var messages = _context.Conversations.Where(m => m.SenderId == curUserId || m.SenderId == id).OrderBy(m => m.CreatedAt).ToList();

            // sent them to the view
            var chat = new ChatViewModel()
            {
                User = reciever,
                Messages = messages,
                Reciever = id,
            };
            return View(chat);
        }

        // send new message
        [HttpPost]
        public async Task<IActionResult> PrivateChat([Bind("ReceiverId , Message")] Conversation newMessage)
        {
            // get the new message
            if (ModelState.IsValid)
            {
                newMessage.Id = new Guid();
                newMessage.SenderId = _userManager.GetUserId(User);
                newMessage.CreatedAt = DateTime.Now;
            }

            // Inside the method where you handle and save the message
            var chatHub = _hubContext.Clients.User(newMessage.ReceiverId);
            await chatHub.SendAsync("ReceiveMessage", newMessage.SenderId, newMessage.Message);


            // add to the database
            await _context.Conversations.AddAsync(newMessage);
            _context.SaveChangesAsync();
            // redirct the chat page for now
            return RedirectToAction("PrivateChat");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}