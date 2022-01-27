using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN.Contract
{


    public partial class CurrentEpochFunction : CurrentEpochFunctionBase { }

    [Function("currentEpoch", "uint256")]
    public class CurrentEpochFunctionBase : FunctionMessage
    {

    }
    public partial class ClaimableFunction : ClaimableFunctionBase { }

    [Function("claimable", "bool")]
    public class ClaimableFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("address", "user", 2)]
        public virtual string User { get; set; }
    }

    public partial class RefundableFunction : RefundableFunctionBase { }

    [Function("refundable", "bool")]
    public class RefundableFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("address", "user", 2)]
        public virtual string User { get; set; }
    }


    public partial class LedgerOutputDTO : LedgerOutputDTOBase { }

    [FunctionOutput]
    public class LedgerOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint8", "position", 1)]
        public virtual byte Position { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("bool", "claimed", 3)]
        public virtual bool Claimed { get; set; }
    }


    public partial class ClaimFunction : ClaimFunctionBase { }

    [Function("claim")]
    public class ClaimFunctionBase : FunctionMessage
    {
        [Parameter("uint256[]", "epochs", 1)]
        public virtual List<BigInteger> Epochs { get; set; }
    }


    public partial class LedgerFunction : LedgerFunctionBase { }

    [Function("ledger", typeof(LedgerOutputDTO))]
    public class LedgerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    [Function("rounds", typeof(RoundsOutput))]
    public class RoundsMsg : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger roundID { get; set; }
    }

    [FunctionOutput]
    public class RoundsOutput : IFunctionOutputDTO
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "startTimestamp", 2)]
        public virtual BigInteger StartTimestamp { get; set; }
        [Parameter("uint256", "lockTimestamp", 3)]
        public virtual BigInteger LockTimestamp { get; set; }
        [Parameter("uint256", "closeTimestamp", 4)]
        public virtual BigInteger CloseTimestamp { get; set; }
        [Parameter("int256", "lockPrice", 5)]
        public virtual BigInteger LockPrice { get; set; }
        [Parameter("int256", "closePrice", 6)]
        public virtual BigInteger ClosePrice { get; set; }
        [Parameter("uint256", "lockOracleId", 7)]
        public virtual BigInteger LockOracleId { get; set; }
        [Parameter("uint256", "closeOracleId", 8)]
        public virtual BigInteger CloseOracleId { get; set; }
        [Parameter("uint256", "totalAmount", 9)]
        public virtual BigInteger TotalAmount { get; set; }
        [Parameter("uint256", "bullAmount", 10)]
        public virtual BigInteger BullAmount { get; set; }
        [Parameter("uint256", "bearAmount", 11)]
        public virtual BigInteger BearAmount { get; set; }
        [Parameter("uint256", "rewardBaseCalAmount", 12)]
        public virtual BigInteger RewardBaseCalAmount { get; set; }
        [Parameter("uint256", "rewardAmount", 13)]
        public virtual BigInteger RewardAmount { get; set; }
        [Parameter("bool", "oracleCalled", 14)]
        public virtual bool OracleCalled { get; set; }
    }



    [Function("betBear")]
    public class BetBearMsg : FunctionMessage
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
    }


    [Function("betBull")]
    public class BetBullMsg : FunctionMessage
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
    }
}
