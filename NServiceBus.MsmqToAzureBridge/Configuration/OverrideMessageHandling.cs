using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;
using NServiceBus.Unicast.Messages;

namespace NServiceBus.MsmqToAzureBridge.Configuration
{
    public class OverrideMessageHandling : IPipelineOverride
    {
        public void Override(BehaviorList<ReceivePhysicalMessageContext> behaviorList)
        {
            behaviorList.Replace<ExtractLogicalMessagesBehavior, RelayTransportMessageToAzureBehaviour>();
        }

        public void Override(BehaviorList<SendPhysicalMessageContext> behaviorList){}

        public void Override(BehaviorList<SendLogicalMessagesContext> behaviorList){}

        public void Override(BehaviorList<SendLogicalMessageContext> behaviorList){}

        public void Override(BehaviorList<ReceiveLogicalMessageContext> behaviorList){}

        public void Override(BehaviorList<HandlerInvocationContext> behaviorList){}
    }
}