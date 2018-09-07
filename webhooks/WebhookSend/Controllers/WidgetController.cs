using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebhookSend.Services;

namespace WebhookSend.Controllers
{
    [Route("api/[controller]")]
    public class WidgetController : Controller
    {
        private readonly IMessageQueueService _messageQueueService;

        public WidgetController(IMessageQueueService messageQueueService)
        {
            _messageQueueService = messageQueueService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/widget
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
                {
                    string body = stream.ReadToEnd();
                    _messageQueueService.SendMessage(body);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
