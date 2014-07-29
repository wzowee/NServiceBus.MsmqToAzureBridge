using System;
using NServiceBus.Azure.Transports.WindowsAzureServiceBus;
using NServiceBus.Logging;
using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;

namespace NServiceBus.MsmqToAzureBridge
{
    public class RelayTransportMessageToAzureBehaviour : IBehavior<ReceivePhysicalMessageContext>
    {
        readonly static ILog Logger = LogManager.GetLogger(typeof(RelayTransportMessageToAzureBehaviour));
        private readonly AzureServiceBusTopicPublisher _publisher;

        public RelayTransportMessageToAzureBehaviour()
        {
            _publisher = new AzureServiceBusTopicPublisher{ TopicClientCreator = new AzureServiceBusTopicClientCreator() };
        }

        public void Invoke(ReceivePhysicalMessageContext context, Action next)
        {
            Logger.Info("Publishing to Azure with type " + context.PhysicalMessage.Headers[Headers.EnclosedMessageTypes]);

            try
            {
                if (!_publisher.Publish(context.PhysicalMessage, null))
                    throw new Exception("Failed to publish to azure as no client could be created");    
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to relay to Azure.", ex);
                throw;
            }
        }
    }
}