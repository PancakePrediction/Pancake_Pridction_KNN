using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace fucktetst.pancake_Prediction.ContractDefinition
{
    public partial class BetInfo : BetInfoBase { }

    public class BetInfoBase 
    {
        [Parameter("uint8", "position", 1)]
        public virtual byte Position { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("bool", "claimed", 3)]
        public virtual bool Claimed { get; set; }
    }
}
