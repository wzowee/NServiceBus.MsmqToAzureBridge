
using Autofac;
using NServiceBus.AutomaticSubscriptions;
using NServiceBus.Azure.Transports.WindowsAzureServiceBus;

namespace NServiceBus.MsmqToAzureBridge
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HandlerLessAutoSubscriptionStrategy>().As<IAutoSubscriptionStrategy>();
            builder.RegisterType<AzureServiceBusTopicClientCreator>().As<ICreateTopicClients>();

            var container = builder.Build();


            Configure.Serialization.Json();
            Configure.With()
                     .AutofacBuilder(container)
                     .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Events"));

        }
    }
}
