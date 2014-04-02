using NServiceBus.AutomaticSubscriptions;
using NServiceBus.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NServiceBus.MsmqToAzureBridge
{
    public class HandlerLessAutoSubscriptionStrategy : IAutoSubscriptionStrategy
    {
        public IEnumerable<Type> GetEventsToSubscribe()
        {
            var unicastConfig = Configure.GetConfigSection<UnicastBusConfig>();

            var messageEndpointMappings = unicastConfig.MessageEndpointMappings.Cast<MessageEndpointMapping>()
               .OrderByDescending(m => m)
               .ToList();

            var eventsToSubscribe = new List<Type>();

            foreach (var mapping in messageEndpointMappings)
            {
                mapping.Configure((messageType, address) =>
                    {
                        if (MessageConventionExtensions.IsEventType(messageType))
                            eventsToSubscribe.Add(messageType);
                    });
            }

            return eventsToSubscribe;
        }
    }
}