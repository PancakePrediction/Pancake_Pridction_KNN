using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.JsonRpc.WebSocketStreamingClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Reactive.Eth.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN.Contract
{
    public class ContractEventSubscriber
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        StreamingWebSocketClient _client;
        Nethereum.Contracts.Contract contract;
        string proxy;
        List<string> topics = new List<string>();
        public ContractEventSubscriber(string websocketNode, Nethereum.Contracts.Contract contract, string proxy, Common.Logging.ILog log)
        {
            this.proxy = proxy;
            this.contract = contract;
            //LogManager
            _client = new StreamingWebSocketClient(websocketNode, /*proxy,*/ log: log);
            //_client.RequestHeaders.Add("x-api-key", "f12644f7-c8e9-443e-a303-f9949c8fda88");
            _client.StartAsync().Wait();
            //var conncted = _client.StartWebsocket();
        }
        public async Task<EthLogsObservableSubscription> SubscribEvent<T>(Action<FilterLog> onNext) where T : IEventDTO, new()
        {
            //var ffff = contract.GetEvent("BetBull").CreateFilterInput();
            var filter = contract.GetEvent<T>().CreateFilterInput();

            var subscription = new EthLogsObservableSubscription(_client);
            _disposables.Add(subscription.GetSubscriptionDataResponsesAsObservable().Subscribe(onNext));
            await subscription.SubscribeAsync(filter);
            return subscription;
        }


        public void PreSubscribEvent<T>() where T : IEventDTO, new()
        {
            var filter = contract.GetEvent<T>().CreateFilterInput();
            topics.Add(filter.Topics[0].ToString());
        }

        public async Task<EthLogsObservableSubscription> SubscribEventsASync(Action<FilterLog> onNext)
        {
            if (topics.Count==0)
                throw new Exception("Should use PreSubscribEvent first");

            var filter = new NewFilterInput();
            filter.Address = new string[] { contract.Address };
            filter.FromBlock = BlockParameter.CreateEarliest();
            filter.ToBlock = BlockParameter.CreateLatest();            
            filter.Topics = topics.ToArray();

            var subscription = new EthLogsObservableSubscription(_client);
            _disposables.Add(subscription.GetSubscriptionDataResponsesAsObservable().Subscribe(onNext));
            await subscription.SubscribeAsync(filter);
            return subscription;
        }
    }
}
