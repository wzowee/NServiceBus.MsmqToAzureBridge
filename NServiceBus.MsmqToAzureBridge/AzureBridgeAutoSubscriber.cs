using System.Linq;
using NServiceBus.AutomaticSubscriptions;
using NServiceBus.Logging;

namespace NServiceBus.MsmqToAzureBridge
{
    public class AzureBridgeAutoSubscriber : IWantToRunWhenBusStartsAndStops
    {
        public IAutoSubscriptionStrategy AutoSubscriptionStrategy { get; set; }

        public IBus Bus { get; set; }

        public void Start()
        {
            foreach (var eventType in AutoSubscriptionStrategy.GetEventsToSubscribe()
                    .Where(t => !MessageConventionExtensions.IsInSystemConventionList(t)))
            {
                Bus.Subscribe(eventType);
                Logger.DebugFormat("Auto subscribed to event {0}", eventType);
            }
        }

        public void Stop()
        {
            
        }

        readonly static ILog Logger = LogManager.GetLogger(typeof(HandlerLessAutoSubscriptionStrategy));
    }
}