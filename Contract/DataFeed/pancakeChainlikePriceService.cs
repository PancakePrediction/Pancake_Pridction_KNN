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
    public partial class pancakeChainlikePriceService
    {
        protected Nethereum.Web3.Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public pancakeChainlikePriceService()
        {
            string contractAddress = "0xd276fcf34d54a926773c399ebaa772c12ec394ac";
            Web3 = new Web3("https://nodes.pancakeswap.com");
            ContractHandler = Web3.Eth.GetContractHandler(contractAddress);
        }

        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, Abi_94acDeployment abi_94acDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<Abi_94acDeployment>().SendRequestAndWaitForReceiptAsync(abi_94acDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, Abi_94acDeployment abi_94acDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<Abi_94acDeployment>().SendRequestAsync(abi_94acDeployment);
        }

        public Task<string> AcceptOwnershipRequestAsync(AcceptOwnershipFunction acceptOwnershipFunction)
        {
            return ContractHandler.SendRequestAsync(acceptOwnershipFunction);
        }


        public Task<string> AcceptOwnershipRequestAsync()
        {
            return ContractHandler.SendRequestAsync<AcceptOwnershipFunction>();
        }

        public Task<TransactionReceipt> AcceptOwnershipRequestAndWaitForReceiptAsync(AcceptOwnershipFunction acceptOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> AcceptOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync<AcceptOwnershipFunction>(null, cancellationToken);
        }

        public Task<string> AccessControllerQueryAsync(AccessControllerFunction accessControllerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AccessControllerFunction, string>(accessControllerFunction, blockParameter);
        }


        public Task<string> AccessControllerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AccessControllerFunction, string>(null, blockParameter);
        }

        public Task<string> AggregatorQueryAsync(AggregatorFunction aggregatorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AggregatorFunction, string>(aggregatorFunction, blockParameter);
        }


        public Task<string> AggregatorQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AggregatorFunction, string>(null, blockParameter);
        }

        public Task<string> ConfirmAggregatorRequestAsync(ConfirmAggregatorFunction confirmAggregatorFunction)
        {
            return ContractHandler.SendRequestAsync(confirmAggregatorFunction);
        }

        public Task<TransactionReceipt> ConfirmAggregatorRequestAndWaitForReceiptAsync(ConfirmAggregatorFunction confirmAggregatorFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmAggregatorFunction, cancellationToken);
        }

        public Task<string> ConfirmAggregatorRequestAsync(string aggregator)
        {
            var confirmAggregatorFunction = new ConfirmAggregatorFunction();
            confirmAggregatorFunction.Aggregator = aggregator;

            return ContractHandler.SendRequestAsync(confirmAggregatorFunction);
        }

        public Task<TransactionReceipt> ConfirmAggregatorRequestAndWaitForReceiptAsync(string aggregator, CancellationTokenSource cancellationToken = null)
        {
            var confirmAggregatorFunction = new ConfirmAggregatorFunction();
            confirmAggregatorFunction.Aggregator = aggregator;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmAggregatorFunction, cancellationToken);
        }

        public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction, blockParameter);
        }


        public Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(null, blockParameter);
        }

        public Task<string> DescriptionQueryAsync(DescriptionFunction descriptionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(descriptionFunction, blockParameter);
        }


        public Task<string> DescriptionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DescriptionFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetAnswerQueryAsync(GetAnswerFunction getAnswerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAnswerFunction, BigInteger>(getAnswerFunction, blockParameter);
        }


        public Task<BigInteger> GetAnswerQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getAnswerFunction = new GetAnswerFunction();
            getAnswerFunction.RoundId = roundId;

            return ContractHandler.QueryAsync<GetAnswerFunction, BigInteger>(getAnswerFunction, blockParameter);
        }

        public Task<GetRoundDataOutputDTO> GetRoundDataQueryAsync(GetRoundDataFunction getRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetRoundDataFunction, GetRoundDataOutputDTO>(getRoundDataFunction, blockParameter);
        }

        public Task<GetRoundDataOutputDTO> GetRoundDataQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getRoundDataFunction = new GetRoundDataFunction();
            getRoundDataFunction.RoundId = roundId;

            return ContractHandler.QueryDeserializingToObjectAsync<GetRoundDataFunction, GetRoundDataOutputDTO>(getRoundDataFunction, blockParameter);
        }

        public Task<BigInteger> GetTimestampQueryAsync(GetTimestampFunction getTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTimestampFunction, BigInteger>(getTimestampFunction, blockParameter);
        }


        public Task<BigInteger> GetTimestampQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getTimestampFunction = new GetTimestampFunction();
            getTimestampFunction.RoundId = roundId;

            return ContractHandler.QueryAsync<GetTimestampFunction, BigInteger>(getTimestampFunction, blockParameter);
        }

        public Task<BigInteger> LatestAnswerQueryAsync(LatestAnswerFunction latestAnswerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestAnswerFunction, BigInteger>(latestAnswerFunction, blockParameter);
        }


        public Task<BigInteger> LatestAnswerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestAnswerFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> LatestRoundQueryAsync(LatestRoundFunction latestRoundFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestRoundFunction, BigInteger>(latestRoundFunction, blockParameter);
        }


        public Task<BigInteger> LatestRoundQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestRoundFunction, BigInteger>(null, blockParameter);
        }

        public Task<LatestRoundDataOutputDTO> LatestRoundDataQueryAsync(LatestRoundDataFunction latestRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LatestRoundDataFunction, LatestRoundDataOutputDTO>(latestRoundDataFunction, blockParameter);
        }

        public Task<LatestRoundDataOutputDTO> LatestRoundDataQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LatestRoundDataFunction, LatestRoundDataOutputDTO>(null, blockParameter);
        }

        public Task<BigInteger> LatestTimestampQueryAsync(LatestTimestampFunction latestTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestTimestampFunction, BigInteger>(latestTimestampFunction, blockParameter);
        }


        public Task<BigInteger> LatestTimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LatestTimestampFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }


        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> PhaseAggregatorsQueryAsync(PhaseAggregatorsFunction phaseAggregatorsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PhaseAggregatorsFunction, string>(phaseAggregatorsFunction, blockParameter);
        }


        public Task<string> PhaseAggregatorsQueryAsync(ushort returnValue1, BlockParameter blockParameter = null)
        {
            var phaseAggregatorsFunction = new PhaseAggregatorsFunction();
            phaseAggregatorsFunction.ReturnValue1 = returnValue1;

            return ContractHandler.QueryAsync<PhaseAggregatorsFunction, string>(phaseAggregatorsFunction, blockParameter);
        }

        public Task<ushort> PhaseIdQueryAsync(PhaseIdFunction phaseIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PhaseIdFunction, ushort>(phaseIdFunction, blockParameter);
        }


        public Task<ushort> PhaseIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PhaseIdFunction, ushort>(null, blockParameter);
        }

        public Task<string> ProposeAggregatorRequestAsync(ProposeAggregatorFunction proposeAggregatorFunction)
        {
            return ContractHandler.SendRequestAsync(proposeAggregatorFunction);
        }

        public Task<TransactionReceipt> ProposeAggregatorRequestAndWaitForReceiptAsync(ProposeAggregatorFunction proposeAggregatorFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(proposeAggregatorFunction, cancellationToken);
        }

        public Task<string> ProposeAggregatorRequestAsync(string aggregator)
        {
            var proposeAggregatorFunction = new ProposeAggregatorFunction();
            proposeAggregatorFunction.Aggregator = aggregator;

            return ContractHandler.SendRequestAsync(proposeAggregatorFunction);
        }

        public Task<TransactionReceipt> ProposeAggregatorRequestAndWaitForReceiptAsync(string aggregator, CancellationTokenSource cancellationToken = null)
        {
            var proposeAggregatorFunction = new ProposeAggregatorFunction();
            proposeAggregatorFunction.Aggregator = aggregator;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(proposeAggregatorFunction, cancellationToken);
        }

        public Task<string> ProposedAggregatorQueryAsync(ProposedAggregatorFunction proposedAggregatorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposedAggregatorFunction, string>(proposedAggregatorFunction, blockParameter);
        }


        public Task<string> ProposedAggregatorQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposedAggregatorFunction, string>(null, blockParameter);
        }

        public Task<ProposedGetRoundDataOutputDTO> ProposedGetRoundDataQueryAsync(ProposedGetRoundDataFunction proposedGetRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ProposedGetRoundDataFunction, ProposedGetRoundDataOutputDTO>(proposedGetRoundDataFunction, blockParameter);
        }

        public Task<ProposedGetRoundDataOutputDTO> ProposedGetRoundDataQueryAsync(BigInteger roundId, BlockParameter blockParameter = null)
        {
            var proposedGetRoundDataFunction = new ProposedGetRoundDataFunction();
            proposedGetRoundDataFunction.RoundId = roundId;

            return ContractHandler.QueryDeserializingToObjectAsync<ProposedGetRoundDataFunction, ProposedGetRoundDataOutputDTO>(proposedGetRoundDataFunction, blockParameter);
        }

        public Task<ProposedLatestRoundDataOutputDTO> ProposedLatestRoundDataQueryAsync(ProposedLatestRoundDataFunction proposedLatestRoundDataFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ProposedLatestRoundDataFunction, ProposedLatestRoundDataOutputDTO>(proposedLatestRoundDataFunction, blockParameter);
        }

        public Task<ProposedLatestRoundDataOutputDTO> ProposedLatestRoundDataQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ProposedLatestRoundDataFunction, ProposedLatestRoundDataOutputDTO>(null, blockParameter);
        }

        public Task<string> SetControllerRequestAsync(SetControllerFunction setControllerFunction)
        {
            return ContractHandler.SendRequestAsync(setControllerFunction);
        }

        public Task<TransactionReceipt> SetControllerRequestAndWaitForReceiptAsync(SetControllerFunction setControllerFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setControllerFunction, cancellationToken);
        }

        public Task<string> SetControllerRequestAsync(string accessController)
        {
            var setControllerFunction = new SetControllerFunction();
            setControllerFunction.AccessController = accessController;

            return ContractHandler.SendRequestAsync(setControllerFunction);
        }

        public Task<TransactionReceipt> SetControllerRequestAndWaitForReceiptAsync(string accessController, CancellationTokenSource cancellationToken = null)
        {
            var setControllerFunction = new SetControllerFunction();
            setControllerFunction.AccessController = accessController;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setControllerFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
            return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string to)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
            transferOwnershipFunction.To = to;

            return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string to, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
            transferOwnershipFunction.To = to;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<BigInteger> VersionQueryAsync(VersionFunction versionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VersionFunction, BigInteger>(versionFunction, blockParameter);
        }


        public Task<BigInteger> VersionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VersionFunction, BigInteger>(null, blockParameter);
        }
    }
}
