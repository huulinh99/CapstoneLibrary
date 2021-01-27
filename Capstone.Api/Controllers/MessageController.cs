//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Threading.Tasks;
//using Capstone.Core.Hubs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;

//namespace Capstone.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MessageController : ControllerBase
//    {
//        private readonly MessageHub _messageHub;
//        public MessageController(MessageHub messageHub)
//        {
//            _messageHub = messageHub;
//        }
//        [HttpPost]
//        public IActionResult Create(Message message)
//        {
//            _messageHub.SendMessage(message.Text);
//            return Ok();
//        }
//    }
//}
