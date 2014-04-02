using System;
using NServiceBus.Azure.Transports.WindowsAzureServiceBus;
using NServiceBus.Satellites;
using NServiceBus.Transports.Msmq;
using NServiceBus.Unicast.Transport;

namespace NServiceBus.MsmqToAzureBridge
{
    public class MsmqReceiver : IAdvancedSatellite
    {
        private AzureServiceBusTopicPublisher _publisher;

        public Action<TransportReceiver> GetReceiverCustomization()
        {
            return (tr => { tr.Receiver = new MsmqDequeueStrategy();});
        }

        public bool Disabled
        {
            get { return false; }
        }

        public bool Handle(TransportMessage message)
        {
            _publisher.Publish(message, null);
            return true;
        }

        public Address InputAddress
        {
            get { return Address.Local; }
        }

        public void Start()
        {
            _publisher = new AzureServiceBusTopicPublisher{ TopicClientCreator = new AzureServiceBusTopicClientCreator() };
        }

        public void Stop()
        {
         
        }
    }
}
