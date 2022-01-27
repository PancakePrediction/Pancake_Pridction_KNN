using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN.Contract
{
    [Event("BetBear")]
    public class BetBearEvent : IEventDTO
    {
        [Parameter("address", "sender", 1, true)]
        public string sender { get; set; }

        [Parameter("uint256", "epoch", 2, true)]
        public BigInteger epoch { get; set; }

        [Parameter("uint256", "amount", 3, false)]
        public BigInteger amount { get; set; }
    }
    [Event("BetBull")]
    public class BetBullEvent : IEventDTO
    {
        [Parameter("address", "sender", 1, true)]
        public string sender { get; set; }

        [Parameter("uint256", "epoch", 2, true)]
        public BigInteger epoch { get; set; }

        [Parameter("uint256", "amount", 3, false)]
        public BigInteger amount { get; set; }
    }

    [Event("Claim")]
    public class ClaimEvent : IEventDTO
    {
        [Parameter("address", "sender", 1, true)]
        public string sender { get; set; }

        [Parameter("uint256", "epoch", 2, true)]
        public BigInteger epoch { get; set; }

        [Parameter("uint256", "amount", 3, false)]
        public BigInteger amount { get; set; }
    }


    [Event("EndRound")]
    public class EndRoundEvent : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true)]
        public BigInteger epoch { get; set; }

        [Parameter("uint256", "roundId", 2, true)]
        public BigInteger roundId { get; set; }

        [Parameter("int256", "price", 3, false)]
        public BigInteger price { get; set; }
    }


    [Event("LockRound")]
    public class LockRoundEvent : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true)]
        public BigInteger epoch { get; set; }

        [Parameter("uint256", "roundId", 2, true)]
        public BigInteger roundId { get; set; }

        [Parameter("int256", "price", 3, false)]
        public BigInteger price { get; set; }
    }

    [Event("Pause")]
    public class PauseEvent : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true)]
        public BigInteger epoch { get; set; }
    }

    [Event("Paused")]
    public class PausedEvent : IEventDTO
    {
        [Parameter("address", "account", 1, true)]
        public string account { get; set; }
    }


    [Event("RewardsCalculated")]
    public class RewardsCalculatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true)]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "rewardBaseCalAmount", 2, false)]
        public virtual BigInteger RewardBaseCalAmount { get; set; }
        [Parameter("uint256", "rewardAmount", 3, false)]
        public virtual BigInteger RewardAmount { get; set; }
        [Parameter("uint256", "treasuryAmount", 4, false)]
        public virtual BigInteger TreasuryAmount { get; set; }
    }


    [Event("StartRound")]
    public class StartRoundEvent : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true)]
        public virtual BigInteger Epoch { get; set; }
    }

    [Event("NewBufferAndIntervalSeconds")]
    public class NewBufferAndIntervalSecondsEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "bufferSeconds", 1, false)]
        public virtual BigInteger BufferSeconds { get; set; }
        [Parameter("uint256", "intervalSeconds", 2, false)]
        public virtual BigInteger IntervalSeconds { get; set; }
    }

}
