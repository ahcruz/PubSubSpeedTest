using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PubSubSpeedTest.Common;

namespace PubSubSpeedTest.Worker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var pubsubService = new PubSubService();
            pubsubService.CreateTopic();
            pubsubService.CreateSubscription();

            var simpleSubscriber = SubscriberClient.CreateAsync(PubSubService.SubscriptionName).Result;

            simpleSubscriber.StartAsync((msg, cancellationToken) =>
            {
                Console.WriteLine("{0}: Received new message: {1}", DateTime.Now.ToString("o"), msg.Data.ToStringUtf8());

                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }
    }
}
