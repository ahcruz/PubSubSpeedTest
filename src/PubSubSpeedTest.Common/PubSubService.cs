using System;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Grpc.Core;

namespace PubSubSpeedTest.Common
{
    public class PubSubService
    {
        public static readonly TopicName TopicName = new TopicName("travix-development", "mvincze-speedtest");
        public static readonly SubscriptionName SubscriptionName = new SubscriptionName("travix-development", "mvincze-speedtest-subscription");

        public void CreateTopic()
        {
            var publisherService = PublisherServiceApiClient.Create();

            try
            {
                publisherService.CreateTopic(TopicName);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.AlreadyExists)
            {
                // This is not a problem, it just means the topic already exists.
            }
        }

        public void CreateSubscription()
        {
            var subscriberService = SubscriberServiceApiClient.Create();

            try
            {
                subscriberService.CreateSubscription(SubscriptionName, TopicName, pushConfig: null, ackDeadlineSeconds: 60);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.AlreadyExists)
            {
                // This is not a problem, it just means the subscription already exists.
            }
        }
    }
}
