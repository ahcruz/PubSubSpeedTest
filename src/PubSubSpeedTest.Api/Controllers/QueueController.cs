using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.Mvc;
using PubSubSpeedTest.Common;

namespace PubSubSpeedTest.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpPost("publish")]
        public async Task<IActionResult> Publish([FromBody] string message)
        {
            var randomId = Guid.NewGuid().ToString().Substring(0, 4);
            message = $"{message} {randomId}";

            var publisher = await PublisherClient.CreateAsync(PubSubService.TopicName);

            Console.WriteLine("{0}: Publishing message: {1}", DateTime.Now.ToString("o"), message);
            var messageId = await publisher.PublishAsync(message);
            Console.WriteLine("{0}: Publishing message done", DateTime.Now.ToString("o"));

            await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));

            return Ok("Message published!");
        }
    }
}
