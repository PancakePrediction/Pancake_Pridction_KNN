using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Pancake_Pridction_KNN.Contract;

namespace pancakeChainlikePrice
{
    public partial class pancakeAPIService
    {
        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }
        const string contractAddress = "0x18B2A687610328590Bc8F2e5fEdDe3b582A49cdA";

        public pancakeAPIService()
        {
            Web3 = new Web3("https://nodes.pancakeswap.com");
            ContractHandler = Web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> CurrentEpochQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentEpochFunction, BigInteger>(null, blockParameter);
        }

    }
}
