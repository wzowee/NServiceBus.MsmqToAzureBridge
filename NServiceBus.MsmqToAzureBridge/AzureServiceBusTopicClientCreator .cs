using System.Configuration;
using Microsoft.ServiceBus.Messaging;
using NServiceBus.Azure.Transports.WindowsAzureServiceBus;
using NServiceBus.Config;

namespace NServiceBus.MsmqToAzureBridge
{
    public class AzureServiceBusTopicClientCreator : ICreateTopicClients
    {
        public TopicClient Create(Address address)
        {
            var configSection = Configure.GetConfigSection<AzureServiceBusQueueConfig>();
            if (configSection == null)
            {
                throw new ConfigurationErrorsException("Connection String should be defined in AzureServiceBusQueueConfig config section");
            }

            return TopicClient.CreateFromConnectionString(configSection.ConnectionString, "NServiceBus.MsmqToAzureBridge.events");
        }
    }
}