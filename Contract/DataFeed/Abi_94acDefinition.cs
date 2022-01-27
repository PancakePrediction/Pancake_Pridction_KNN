using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace pancakeChainlikePrice
{


    public partial class Abi_94acDeployment : Abi_94acDeploymentBase
    {
        public Abi_94acDeployment() : base("") { }
        public Abi_94acDeployment(string byteCode) : base(byteCode) { }
    }

    public class Abi_94acDeploymentBase : ContractDeploymentMessage
    {
        //public static string BYTECODE = "[{"inputs":[{"internalType":"address","name":"_aggregator","type":"address"},{"internalType":"address","name":"_accessController","type":"address"}],"stateMutability":"nonpayable","type":"constructor"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"int256","name":"current","type":"int256"},{"indexed":true,"internalType":"uint256","name":"roundId","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"updatedAt","type":"uint256"}],"name":"AnswerUpdated","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"uint256","name":"roundId","type":"uint256"},{"indexed":true,"internalType":"address","name":"startedBy","type":"address"},{"indexed":false,"internalType":"uint256","name":"startedAt","type":"uint256"}],"name":"NewRound","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"from","type":"address"},{"indexed":true,"internalType":"address","name":"to","type":"address"}],"name":"OwnershipTransferRequested","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"from","type":"address"},{"indexed":true,"internalType":"address","name":"to","type":"address"}],"name":"OwnershipTransferred","type":"event"},{"inputs":[],"name":"acceptOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"accessController","outputs":[{"internalType":"contract AccessControllerInterface","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"aggregator","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_aggregator","type":"address"}],"name":"confirmAggregator","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"decimals","outputs":[{"internalType":"uint8","name":"","type":"uint8"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"description","outputs":[{"internalType":"string","name":"","type":"string"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_roundId","type":"uint256"}],"name":"getAnswer","outputs":[{"internalType":"int256","name":"","type":"int256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint80","name":"_roundId","type":"uint80"}],"name":"getRoundData","outputs":[{"internalType":"uint80","name":"roundId","type":"uint80"},{"internalType":"int256","name":"answer","type":"int256"},{"internalType":"uint256","name":"startedAt","type":"uint256"},{"internalType":"uint256","name":"updatedAt","type":"uint256"},{"internalType":"uint80","name":"answeredInRound","type":"uint80"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_roundId","type":"uint256"}],"name":"getTimestamp","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"latestAnswer","outputs":[{"internalType":"int256","name":"","type":"int256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"latestRound","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"latestRoundData","outputs":[{"internalType":"uint80","name":"roundId","type":"uint80"},{"internalType":"int256","name":"answer","type":"int256"},{"internalType":"uint256","name":"startedAt","type":"uint256"},{"internalType":"uint256","name":"updatedAt","type":"uint256"},{"internalType":"uint80","name":"answeredInRound","type":"uint80"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"latestTimestamp","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"owner","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint16","name":"","type":"uint16"}],"name":"phaseAggregators","outputs":[{"internalType":"contract AggregatorV2V3Interface","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"phaseId","outputs":[{"internalType":"uint16","name":"","type":"uint16"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_aggregator","type":"address"}],"name":"proposeAggregator","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"proposedAggregator","outputs":[{"internalType":"contract AggregatorV2V3Interface","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint80","name":"_roundId","type":"uint80"}],"name":"proposedGetRoundData","outputs":[{"internalType":"uint80","name":"roundId","type":"uint80"},{"internalType":"int256","name":"answer","type":"int256"},{"internalType":"uint256","name":"startedAt","type":"uint256"},{"internalType":"uint256","name":"updatedAt","type":"uint256"},{"internalType":"uint80","name":"answeredInRound","type":"uint80"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"proposedLatestRoundData","outputs":[{"internalType":"uint80","name":"roundId","type":"uint80"},{"internalType":"int256","name":"answer","type":"int256"},{"internalType":"uint256","name":"startedAt","type":"uint256"},{"internalType":"uint256","name":"updatedAt","type":"uint256"},{"internalType":"uint80","name":"answeredInRound","type":"uint80"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_accessController","type":"address"}],"name":"setController","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_to","type":"address"}],"name":"transferOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"version","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"}]";
        //public Abi_94acDeploymentBase() : base(BYTECODE) { }
        public Abi_94acDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
        [Parameter("address", "_accessController", 2)]
        public virtual string AccessController { get; set; }
    }

    public partial class AcceptOwnershipFunction : AcceptOwnershipFunctionBase { }

    [Function("acceptOwnership")]
    public class AcceptOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class AccessControllerFunction : AccessControllerFunctionBase { }

    [Function("accessController", "address")]
    public class AccessControllerFunctionBase : FunctionMessage
    {

    }

    public partial class AggregatorFunction : AggregatorFunctionBase { }

    [Function("aggregator", "address")]
    public class AggregatorFunctionBase : FunctionMessage
    {

    }

    public partial class ConfirmAggregatorFunction : ConfirmAggregatorFunctionBase { }

    [Function("confirmAggregator")]
    public class ConfirmAggregatorFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class DescriptionFunction : DescriptionFunctionBase { }

    [Function("description", "string")]
    public class DescriptionFunctionBase : FunctionMessage
    {

    }

    public partial class GetAnswerFunction : GetAnswerFunctionBase { }

    [Function("getAnswer", "int256")]
    public class GetAnswerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class GetRoundDataFunction : GetRoundDataFunctionBase { }

    [Function("getRoundData", typeof(GetRoundDataOutputDTO))]
    public class GetRoundDataFunctionBase : FunctionMessage
    {
        [Parameter("uint80", "_roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class GetTimestampFunction : GetTimestampFunctionBase { }

    [Function("getTimestamp", "uint256")]
    public class GetTimestampFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class LatestAnswerFunction : LatestAnswerFunctionBase { }

    [Function("latestAnswer", "int256")]
    public class LatestAnswerFunctionBase : FunctionMessage
    {

    }

    public partial class LatestRoundFunction : LatestRoundFunctionBase { }

    [Function("latestRound", "uint256")]
    public class LatestRoundFunctionBase : FunctionMessage
    {

    }

    public partial class LatestRoundDataFunction : LatestRoundDataFunctionBase { }

    [Function("latestRoundData", typeof(LatestRoundDataOutputDTO))]
    public class LatestRoundDataFunctionBase : FunctionMessage
    {

    }

    public partial class LatestTimestampFunction : LatestTimestampFunctionBase { }

    [Function("latestTimestamp", "uint256")]
    public class LatestTimestampFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class PhaseAggregatorsFunction : PhaseAggregatorsFunctionBase { }

    [Function("phaseAggregators", "address")]
    public class PhaseAggregatorsFunctionBase : FunctionMessage
    {
        [Parameter("uint16", "", 1)]
        public virtual ushort ReturnValue1 { get; set; }
    }

    public partial class PhaseIdFunction : PhaseIdFunctionBase { }

    [Function("phaseId", "uint16")]
    public class PhaseIdFunctionBase : FunctionMessage
    {

    }

    public partial class ProposeAggregatorFunction : ProposeAggregatorFunctionBase { }

    [Function("proposeAggregator")]
    public class ProposeAggregatorFunctionBase : FunctionMessage
    {
        [Parameter("address", "_aggregator", 1)]
        public virtual string Aggregator { get; set; }
    }

    public partial class ProposedAggregatorFunction : ProposedAggregatorFunctionBase { }

    [Function("proposedAggregator", "address")]
    public class ProposedAggregatorFunctionBase : FunctionMessage
    {

    }

    public partial class ProposedGetRoundDataFunction : ProposedGetRoundDataFunctionBase { }

    [Function("proposedGetRoundData", typeof(ProposedGetRoundDataOutputDTO))]
    public class ProposedGetRoundDataFunctionBase : FunctionMessage
    {
        [Parameter("uint80", "_roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class ProposedLatestRoundDataFunction : ProposedLatestRoundDataFunctionBase { }

    [Function("proposedLatestRoundData", typeof(ProposedLatestRoundDataOutputDTO))]
    public class ProposedLatestRoundDataFunctionBase : FunctionMessage
    {

    }

    public partial class SetControllerFunction : SetControllerFunctionBase { }

    [Function("setController")]
    public class SetControllerFunctionBase : FunctionMessage
    {
        [Parameter("address", "_accessController", 1)]
        public virtual string AccessController { get; set; }
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
    }

    public partial class VersionFunction : VersionFunctionBase { }

    [Function("version", "uint256")]
    public class VersionFunctionBase : FunctionMessage
    {

    }

    public partial class AnswerUpdatedEventDTO : AnswerUpdatedEventDTOBase { }

    [Event("AnswerUpdated")]
    public class AnswerUpdatedEventDTOBase : IEventDTO
    {
        [Parameter("int256", "current", 1, true )]
        public virtual BigInteger Current { get; set; }
        [Parameter("uint256", "roundId", 2, true )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("uint256", "updatedAt", 3, false )]
        public virtual BigInteger UpdatedAt { get; set; }
    }

    public partial class NewRoundEventDTO : NewRoundEventDTOBase { }

    [Event("NewRound")]
    public class NewRoundEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "roundId", 1, true )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("address", "startedBy", 2, true )]
        public virtual string StartedBy { get; set; }
        [Parameter("uint256", "startedAt", 3, false )]
        public virtual BigInteger StartedAt { get; set; }
    }

    public partial class OwnershipTransferRequestedEventDTO : OwnershipTransferRequestedEventDTOBase { }

    [Event("OwnershipTransferRequested")]
    public class OwnershipTransferRequestedEventDTOBase : IEventDTO
    {
        [Parameter("address", "from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2, true )]
        public virtual string To { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2, true )]
        public virtual string To { get; set; }
    }



    public partial class AccessControllerOutputDTO : AccessControllerOutputDTOBase { }

    [FunctionOutput]
    public class AccessControllerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AggregatorOutputDTO : AggregatorOutputDTOBase { }

    [FunctionOutput]
    public class AggregatorOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class DescriptionOutputDTO : DescriptionOutputDTOBase { }

    [FunctionOutput]
    public class DescriptionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetAnswerOutputDTO : GetAnswerOutputDTOBase { }

    [FunctionOutput]
    public class GetAnswerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetRoundDataOutputDTO : GetRoundDataOutputDTOBase { }

    [FunctionOutput]
    public class GetRoundDataOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint80", "roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("int256", "answer", 2)]
        public virtual BigInteger Answer { get; set; }
        [Parameter("uint256", "startedAt", 3)]
        public virtual BigInteger StartedAt { get; set; }
        [Parameter("uint256", "updatedAt", 4)]
        public virtual BigInteger UpdatedAt { get; set; }
        [Parameter("uint80", "answeredInRound", 5)]
        public virtual BigInteger AnsweredInRound { get; set; }
    }

    public partial class GetTimestampOutputDTO : GetTimestampOutputDTOBase { }

    [FunctionOutput]
    public class GetTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class LatestAnswerOutputDTO : LatestAnswerOutputDTOBase { }

    [FunctionOutput]
    public class LatestAnswerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class LatestRoundOutputDTO : LatestRoundOutputDTOBase { }

    [FunctionOutput]
    public class LatestRoundOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class LatestRoundDataOutputDTO : LatestRoundDataOutputDTOBase { }

    [FunctionOutput]
    public class LatestRoundDataOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint80", "roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("int256", "answer", 2)]
        public virtual BigInteger Answer { get; set; }
        [Parameter("uint256", "startedAt", 3)]
        public virtual BigInteger StartedAt { get; set; }
        [Parameter("uint256", "updatedAt", 4)]
        public virtual BigInteger UpdatedAt { get; set; }
        [Parameter("uint80", "answeredInRound", 5)]
        public virtual BigInteger AnsweredInRound { get; set; }
    }

    public partial class LatestTimestampOutputDTO : LatestTimestampOutputDTOBase { }

    [FunctionOutput]
    public class LatestTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class PhaseAggregatorsOutputDTO : PhaseAggregatorsOutputDTOBase { }

    [FunctionOutput]
    public class PhaseAggregatorsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class PhaseIdOutputDTO : PhaseIdOutputDTOBase { }

    [FunctionOutput]
    public class PhaseIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint16", "", 1)]
        public virtual ushort ReturnValue1 { get; set; }
    }



    public partial class ProposedAggregatorOutputDTO : ProposedAggregatorOutputDTOBase { }

    [FunctionOutput]
    public class ProposedAggregatorOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ProposedGetRoundDataOutputDTO : ProposedGetRoundDataOutputDTOBase { }

    [FunctionOutput]
    public class ProposedGetRoundDataOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint80", "roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("int256", "answer", 2)]
        public virtual BigInteger Answer { get; set; }
        [Parameter("uint256", "startedAt", 3)]
        public virtual BigInteger StartedAt { get; set; }
        [Parameter("uint256", "updatedAt", 4)]
        public virtual BigInteger UpdatedAt { get; set; }
        [Parameter("uint80", "answeredInRound", 5)]
        public virtual BigInteger AnsweredInRound { get; set; }
    }

    public partial class ProposedLatestRoundDataOutputDTO : ProposedLatestRoundDataOutputDTOBase { }

    [FunctionOutput]
    public class ProposedLatestRoundDataOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint80", "roundId", 1)]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("int256", "answer", 2)]
        public virtual BigInteger Answer { get; set; }
        [Parameter("uint256", "startedAt", 3)]
        public virtual BigInteger StartedAt { get; set; }
        [Parameter("uint256", "updatedAt", 4)]
        public virtual BigInteger UpdatedAt { get; set; }
        [Parameter("uint80", "answeredInRound", 5)]
        public virtual BigInteger AnsweredInRound { get; set; }
    }





    public partial class VersionOutputDTO : VersionOutputDTOBase { }

    [FunctionOutput]
    public class VersionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
