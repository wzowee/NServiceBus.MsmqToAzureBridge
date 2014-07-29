using System.Transactions;
using NServiceBus.Logging;

namespace NServiceBus.MsmqToAzureBridge.Configuration
{
    /// <summary>
    /// Responsible for disabling DTC and setting the transaction level to IsolationLevel Serializable which is the only
    /// level supported by Azure Service bus. 
    /// </summary>
    public class DisableTransactions : IWantCustomInitialization
    {
        readonly static ILog Logger = LogManager.GetLogger(typeof(DisableTransactions));

        public void Init()
        {
            if (Configure.Instance.DistributorEnabled())
            {
                Logger.Info("Skipping disabling transactions as running as a Distributor");
                return;
            }
            
            Logger.Info("Disabling DTC and setting transaction isolation level to Serializable as running as a stand-alone or worker");
            
            // Tell NServiceBus to not use transactions
            Configure.Transactions.Advanced(a => a.DisableDistributedTransactions());

            // The above still results in a Transaction with a ReadCommitted level, so setting to Serializable as this is the only level that works With Azure ServiceBus
            Configure.Transactions.Advanced(settings => settings.IsolationLevel(IsolationLevel.Serializable));
            
        }
    }
}