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

namespace fucktetst.pancake_Prediction.ContractDefinition
{


    public partial class Pancake_PredictionDeployment : Pancake_PredictionDeploymentBase
    {
        public Pancake_PredictionDeployment() : base(BYTECODE) { }
        public Pancake_PredictionDeployment(string byteCode) : base(byteCode) { }
    }

    public class Pancake_PredictionDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "[
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "_oracleAddress",
        "type": "address"
      },
      {
        "internalType": "address",
        "name": "_adminAddress",
        "type": "address"
      },
      {
        "internalType": "address",
        "name": "_operatorAddress",
        "type": "address"
      },
      {
        "internalType": "uint256",
        "name": "_intervalSeconds",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "_bufferSeconds",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "_minBetAmount",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "_oracleUpdateAllowance",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "_treasuryFee",
        "type": "uint256"
      }
    ],
    "stateMutability": "nonpayable",
    "type": "constructor"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "address",
        "name": "sender",
        "type": "address"
      },
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "amount",
        "type": "uint256"
      }
    ],
    "name": "BetBear",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "address",
        "name": "sender",
        "type": "address"
      },
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "amount",
        "type": "uint256"
      }
    ],
    "name": "BetBull",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "address",
        "name": "sender",
        "type": "address"
      },
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "amount",
        "type": "uint256"
      }
    ],
    "name": "Claim",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "roundId",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "int256",
        "name": "price",
        "type": "int256"
      }
    ],
    "name": "EndRound",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "roundId",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "int256",
        "name": "price",
        "type": "int256"
      }
    ],
    "name": "LockRound",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "address",
        "name": "admin",
        "type": "address"
      }
    ],
    "name": "NewAdminAddress",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "bufferSeconds",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "intervalSeconds",
        "type": "uint256"
      }
    ],
    "name": "NewBufferAndIntervalSeconds",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "minBetAmount",
        "type": "uint256"
      }
    ],
    "name": "NewMinBetAmount",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "address",
        "name": "operator",
        "type": "address"
      }
    ],
    "name": "NewOperatorAddress",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "address",
        "name": "oracle",
        "type": "address"
      }
    ],
    "name": "NewOracle",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "oracleUpdateAllowance",
        "type": "uint256"
      }
    ],
    "name": "NewOracleUpdateAllowance",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "treasuryFee",
        "type": "uint256"
      }
    ],
    "name": "NewTreasuryFee",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "address",
        "name": "previousOwner",
        "type": "address"
      },
      {
        "indexed": true,
        "internalType": "address",
        "name": "newOwner",
        "type": "address"
      }
    ],
    "name": "OwnershipTransferred",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      }
    ],
    "name": "Pause",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "address",
        "name": "account",
        "type": "address"
      }
    ],
    "name": "Paused",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "rewardBaseCalAmount",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "rewardAmount",
        "type": "uint256"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "treasuryAmount",
        "type": "uint256"
      }
    ],
    "name": "RewardsCalculated",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      }
    ],
    "name": "StartRound",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "address",
        "name": "token",
        "type": "address"
      },
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "amount",
        "type": "uint256"
      }
    ],
    "name": "TokenRecovery",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "uint256",
        "name": "amount",
        "type": "uint256"
      }
    ],
    "name": "TreasuryClaim",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": true,
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      }
    ],
    "name": "Unpause",
    "type": "event"
  },
  {
    "anonymous": false,
    "inputs": [
      {
        "indexed": false,
        "internalType": "address",
        "name": "account",
        "type": "address"
      }
    ],
    "name": "Unpaused",
    "type": "event"
  },
  {
    "inputs": [],
    "name": "MAX_TREASURY_FEE",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "adminAddress",
    "outputs": [
      {
        "internalType": "address",
        "name": "",
        "type": "address"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      }
    ],
    "name": "betBear",
    "outputs": [],
    "stateMutability": "payable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      }
    ],
    "name": "betBull",
    "outputs": [],
    "stateMutability": "payable",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "bufferSeconds",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256[]",
        "name": "epochs",
        "type": "uint256[]"
      }
    ],
    "name": "claim",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "claimTreasury",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "internalType": "address",
        "name": "user",
        "type": "address"
      }
    ],
    "name": "claimable",
    "outputs": [
      {
        "internalType": "bool",
        "name": "",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "currentEpoch",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "executeRound",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "genesisLockOnce",
    "outputs": [
      {
        "internalType": "bool",
        "name": "",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "genesisLockRound",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "genesisStartOnce",
    "outputs": [
      {
        "internalType": "bool",
        "name": "",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "genesisStartRound",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "user",
        "type": "address"
      },
      {
        "internalType": "uint256",
        "name": "cursor",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "size",
        "type": "uint256"
      }
    ],
    "name": "getUserRounds",
    "outputs": [
      {
        "internalType": "uint256[]",
        "name": "",
        "type": "uint256[]"
      },
      {
        "components": [
          {
            "internalType": "enum PancakePredictionV2.Position",
            "name": "position",
            "type": "uint8"
          },
          {
            "internalType": "uint256",
            "name": "amount",
            "type": "uint256"
          },
          {
            "internalType": "bool",
            "name": "claimed",
            "type": "bool"
          }
        ],
        "internalType": "struct PancakePredictionV2.BetInfo[]",
        "name": "",
        "type": "tuple[]"
      },
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "user",
        "type": "address"
      }
    ],
    "name": "getUserRoundsLength",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "intervalSeconds",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      },
      {
        "internalType": "address",
        "name": "",
        "type": "address"
      }
    ],
    "name": "ledger",
    "outputs": [
      {
        "internalType": "enum PancakePredictionV2.Position",
        "name": "position",
        "type": "uint8"
      },
      {
        "internalType": "uint256",
        "name": "amount",
        "type": "uint256"
      },
      {
        "internalType": "bool",
        "name": "claimed",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "minBetAmount",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "operatorAddress",
    "outputs": [
      {
        "internalType": "address",
        "name": "",
        "type": "address"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "oracle",
    "outputs": [
      {
        "internalType": "contract AggregatorV3Interface",
        "name": "",
        "type": "address"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "oracleLatestRoundId",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "oracleUpdateAllowance",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "owner",
    "outputs": [
      {
        "internalType": "address",
        "name": "",
        "type": "address"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "pause",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "paused",
    "outputs": [
      {
        "internalType": "bool",
        "name": "",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "_token",
        "type": "address"
      },
      {
        "internalType": "uint256",
        "name": "_amount",
        "type": "uint256"
      }
    ],
    "name": "recoverToken",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "internalType": "address",
        "name": "user",
        "type": "address"
      }
    ],
    "name": "refundable",
    "outputs": [
      {
        "internalType": "bool",
        "name": "",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "renounceOwnership",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "name": "rounds",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "epoch",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "startTimestamp",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "lockTimestamp",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "closeTimestamp",
        "type": "uint256"
      },
      {
        "internalType": "int256",
        "name": "lockPrice",
        "type": "int256"
      },
      {
        "internalType": "int256",
        "name": "closePrice",
        "type": "int256"
      },
      {
        "internalType": "uint256",
        "name": "lockOracleId",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "closeOracleId",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "totalAmount",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "bullAmount",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "bearAmount",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "rewardBaseCalAmount",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "rewardAmount",
        "type": "uint256"
      },
      {
        "internalType": "bool",
        "name": "oracleCalled",
        "type": "bool"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "_adminAddress",
        "type": "address"
      }
    ],
    "name": "setAdmin",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "_bufferSeconds",
        "type": "uint256"
      },
      {
        "internalType": "uint256",
        "name": "_intervalSeconds",
        "type": "uint256"
      }
    ],
    "name": "setBufferAndIntervalSeconds",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "_minBetAmount",
        "type": "uint256"
      }
    ],
    "name": "setMinBetAmount",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "_operatorAddress",
        "type": "address"
      }
    ],
    "name": "setOperator",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "_oracle",
        "type": "address"
      }
    ],
    "name": "setOracle",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "_oracleUpdateAllowance",
        "type": "uint256"
      }
    ],
    "name": "setOracleUpdateAllowance",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "uint256",
        "name": "_treasuryFee",
        "type": "uint256"
      }
    ],
    "name": "setTreasuryFee",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "newOwner",
        "type": "address"
      }
    ],
    "name": "transferOwnership",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "treasuryAmount",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "treasuryFee",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  },
  {
    "inputs": [],
    "name": "unpause",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      {
        "internalType": "address",
        "name": "",
        "type": "address"
      },
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "name": "userRounds",
    "outputs": [
      {
        "internalType": "uint256",
        "name": "",
        "type": "uint256"
      }
    ],
    "stateMutability": "view",
    "type": "function"
  }
]";
        public Pancake_PredictionDeploymentBase() : base(BYTECODE) { }
        public Pancake_PredictionDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_oracleAddress", 1)]
        public virtual string OracleAddress { get; set; }
        [Parameter("address", "_adminAddress", 2)]
        public virtual string AdminAddress { get; set; }
        [Parameter("address", "_operatorAddress", 3)]
        public virtual string OperatorAddress { get; set; }
        [Parameter("uint256", "_intervalSeconds", 4)]
        public virtual BigInteger IntervalSeconds { get; set; }
        [Parameter("uint256", "_bufferSeconds", 5)]
        public virtual BigInteger BufferSeconds { get; set; }
        [Parameter("uint256", "_minBetAmount", 6)]
        public virtual BigInteger MinBetAmount { get; set; }
        [Parameter("uint256", "_oracleUpdateAllowance", 7)]
        public virtual BigInteger OracleUpdateAllowance { get; set; }
        [Parameter("uint256", "_treasuryFee", 8)]
        public virtual BigInteger TreasuryFee { get; set; }
    }

    public partial class MAX_TREASURY_FEEFunction : MAX_TREASURY_FEEFunctionBase { }

    [Function("MAX_TREASURY_FEE", "uint256")]
    public class MAX_TREASURY_FEEFunctionBase : FunctionMessage
    {

    }

    public partial class AdminAddressFunction : AdminAddressFunctionBase { }

    [Function("adminAddress", "address")]
    public class AdminAddressFunctionBase : FunctionMessage
    {

    }

    public partial class BetBearFunction : BetBearFunctionBase { }

    [Function("betBear")]
    public class BetBearFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
    }

    public partial class BetBullFunction : BetBullFunctionBase { }

    [Function("betBull")]
    public class BetBullFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "epoch", 1)]
        public virtual BigInteger Epoch { get; set; }
    }

    public partial class BufferSecondsFunction : BufferSecondsFunctionBase { }

    [Function("bufferSeconds", "uint256")]
    public class BufferSecondsFunctionBase : FunctionMessage
    {

    }

    public partial class ClaimFunction : ClaimFunctionBase { }

    [Function("claim")]
    public class ClaimFunctionBase : FunctionMessage
    {
        [Parameter("uint256[]", "epochs", 1)]
        public virtual List<BigInteger> Epochs { get; set; }
    }

    public partial class ClaimTreasuryFunction : ClaimTreasuryFunctionBase { }

    [Function("claimTreasury")]
    public class ClaimTreasuryFunctionBase : FunctionMessage
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

    public partial class CurrentEpochFunction : CurrentEpochFunctionBase { }

    [Function("currentEpoch", "uint256")]
    public class CurrentEpochFunctionBase : FunctionMessage
    {

    }

    public partial class ExecuteRoundFunction : ExecuteRoundFunctionBase { }

    [Function("executeRound")]
    public class ExecuteRoundFunctionBase : FunctionMessage
    {

    }

    public partial class GenesisLockOnceFunction : GenesisLockOnceFunctionBase { }

    [Function("genesisLockOnce", "bool")]
    public class GenesisLockOnceFunctionBase : FunctionMessage
    {

    }

    public partial class GenesisLockRoundFunction : GenesisLockRoundFunctionBase { }

    [Function("genesisLockRound")]
    public class GenesisLockRoundFunctionBase : FunctionMessage
    {

    }

    public partial class GenesisStartOnceFunction : GenesisStartOnceFunctionBase { }

    [Function("genesisStartOnce", "bool")]
    public class GenesisStartOnceFunctionBase : FunctionMessage
    {

    }

    public partial class GenesisStartRoundFunction : GenesisStartRoundFunctionBase { }

    [Function("genesisStartRound")]
    public class GenesisStartRoundFunctionBase : FunctionMessage
    {

    }

    public partial class GetUserRoundsFunction : GetUserRoundsFunctionBase { }

    [Function("getUserRounds", typeof(GetUserRoundsOutputDTO))]
    public class GetUserRoundsFunctionBase : FunctionMessage
    {
        [Parameter("address", "user", 1)]
        public virtual string User { get; set; }
        [Parameter("uint256", "cursor", 2)]
        public virtual BigInteger Cursor { get; set; }
        [Parameter("uint256", "size", 3)]
        public virtual BigInteger Size { get; set; }
    }

    public partial class GetUserRoundsLengthFunction : GetUserRoundsLengthFunctionBase { }

    [Function("getUserRoundsLength", "uint256")]
    public class GetUserRoundsLengthFunctionBase : FunctionMessage
    {
        [Parameter("address", "user", 1)]
        public virtual string User { get; set; }
    }

    public partial class IntervalSecondsFunction : IntervalSecondsFunctionBase { }

    [Function("intervalSeconds", "uint256")]
    public class IntervalSecondsFunctionBase : FunctionMessage
    {

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

    public partial class MinBetAmountFunction : MinBetAmountFunctionBase { }

    [Function("minBetAmount", "uint256")]
    public class MinBetAmountFunctionBase : FunctionMessage
    {

    }

    public partial class OperatorAddressFunction : OperatorAddressFunctionBase { }

    [Function("operatorAddress", "address")]
    public class OperatorAddressFunctionBase : FunctionMessage
    {

    }

    public partial class OracleFunction : OracleFunctionBase { }

    [Function("oracle", "address")]
    public class OracleFunctionBase : FunctionMessage
    {

    }

    public partial class OracleLatestRoundIdFunction : OracleLatestRoundIdFunctionBase { }

    [Function("oracleLatestRoundId", "uint256")]
    public class OracleLatestRoundIdFunctionBase : FunctionMessage
    {

    }

    public partial class OracleUpdateAllowanceFunction : OracleUpdateAllowanceFunctionBase { }

    [Function("oracleUpdateAllowance", "uint256")]
    public class OracleUpdateAllowanceFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class PauseFunction : PauseFunctionBase { }

    [Function("pause")]
    public class PauseFunctionBase : FunctionMessage
    {

    }

    public partial class PausedFunction : PausedFunctionBase { }

    [Function("paused", "bool")]
    public class PausedFunctionBase : FunctionMessage
    {

    }

    public partial class RecoverTokenFunction : RecoverTokenFunctionBase { }

    [Function("recoverToken")]
    public class RecoverTokenFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
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

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class RoundsFunction : RoundsFunctionBase { }

    [Function("rounds", typeof(RoundsOutputDTO))]
    public class RoundsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SetAdminFunction : SetAdminFunctionBase { }

    [Function("setAdmin")]
    public class SetAdminFunctionBase : FunctionMessage
    {
        [Parameter("address", "_adminAddress", 1)]
        public virtual string AdminAddress { get; set; }
    }

    public partial class SetBufferAndIntervalSecondsFunction : SetBufferAndIntervalSecondsFunctionBase { }

    [Function("setBufferAndIntervalSeconds")]
    public class SetBufferAndIntervalSecondsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_bufferSeconds", 1)]
        public virtual BigInteger BufferSeconds { get; set; }
        [Parameter("uint256", "_intervalSeconds", 2)]
        public virtual BigInteger IntervalSeconds { get; set; }
    }

    public partial class SetMinBetAmountFunction : SetMinBetAmountFunctionBase { }

    [Function("setMinBetAmount")]
    public class SetMinBetAmountFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_minBetAmount", 1)]
        public virtual BigInteger MinBetAmount { get; set; }
    }

    public partial class SetOperatorFunction : SetOperatorFunctionBase { }

    [Function("setOperator")]
    public class SetOperatorFunctionBase : FunctionMessage
    {
        [Parameter("address", "_operatorAddress", 1)]
        public virtual string OperatorAddress { get; set; }
    }

    public partial class SetOracleFunction : SetOracleFunctionBase { }

    [Function("setOracle")]
    public class SetOracleFunctionBase : FunctionMessage
    {
        [Parameter("address", "_oracle", 1)]
        public virtual string Oracle { get; set; }
    }

    public partial class SetOracleUpdateAllowanceFunction : SetOracleUpdateAllowanceFunctionBase { }

    [Function("setOracleUpdateAllowance")]
    public class SetOracleUpdateAllowanceFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_oracleUpdateAllowance", 1)]
        public virtual BigInteger OracleUpdateAllowance { get; set; }
    }

    public partial class SetTreasuryFeeFunction : SetTreasuryFeeFunctionBase { }

    [Function("setTreasuryFee")]
    public class SetTreasuryFeeFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_treasuryFee", 1)]
        public virtual BigInteger TreasuryFee { get; set; }
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class TreasuryAmountFunction : TreasuryAmountFunctionBase { }

    [Function("treasuryAmount", "uint256")]
    public class TreasuryAmountFunctionBase : FunctionMessage
    {

    }

    public partial class TreasuryFeeFunction : TreasuryFeeFunctionBase { }

    [Function("treasuryFee", "uint256")]
    public class TreasuryFeeFunctionBase : FunctionMessage
    {

    }

    public partial class UnpauseFunction : UnpauseFunctionBase { }

    [Function("unpause")]
    public class UnpauseFunctionBase : FunctionMessage
    {

    }

    public partial class UserRoundsFunction : UserRoundsFunctionBase { }

    [Function("userRounds", "uint256")]
    public class UserRoundsFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }

    public partial class BetBearEventDTO : BetBearEventDTOBase { }

    [Event("BetBear")]
    public class BetBearEventDTOBase : IEventDTO
    {
        [Parameter("address", "sender", 1, true )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "epoch", 2, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class BetBullEventDTO : BetBullEventDTOBase { }

    [Event("BetBull")]
    public class BetBullEventDTOBase : IEventDTO
    {
        [Parameter("address", "sender", 1, true )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "epoch", 2, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class ClaimEventDTO : ClaimEventDTOBase { }

    [Event("Claim")]
    public class ClaimEventDTOBase : IEventDTO
    {
        [Parameter("address", "sender", 1, true )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "epoch", 2, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class EndRoundEventDTO : EndRoundEventDTOBase { }

    [Event("EndRound")]
    public class EndRoundEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "roundId", 2, true )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("int256", "price", 3, false )]
        public virtual BigInteger Price { get; set; }
    }

    public partial class LockRoundEventDTO : LockRoundEventDTOBase { }

    [Event("LockRound")]
    public class LockRoundEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "roundId", 2, true )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("int256", "price", 3, false )]
        public virtual BigInteger Price { get; set; }
    }

    public partial class NewAdminAddressEventDTO : NewAdminAddressEventDTOBase { }

    [Event("NewAdminAddress")]
    public class NewAdminAddressEventDTOBase : IEventDTO
    {
        [Parameter("address", "admin", 1, false )]
        public virtual string Admin { get; set; }
    }

    public partial class NewBufferAndIntervalSecondsEventDTO : NewBufferAndIntervalSecondsEventDTOBase { }

    [Event("NewBufferAndIntervalSeconds")]
    public class NewBufferAndIntervalSecondsEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "bufferSeconds", 1, false )]
        public virtual BigInteger BufferSeconds { get; set; }
        [Parameter("uint256", "intervalSeconds", 2, false )]
        public virtual BigInteger IntervalSeconds { get; set; }
    }

    public partial class NewMinBetAmountEventDTO : NewMinBetAmountEventDTOBase { }

    [Event("NewMinBetAmount")]
    public class NewMinBetAmountEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "minBetAmount", 2, false )]
        public virtual BigInteger MinBetAmount { get; set; }
    }

    public partial class NewOperatorAddressEventDTO : NewOperatorAddressEventDTOBase { }

    [Event("NewOperatorAddress")]
    public class NewOperatorAddressEventDTOBase : IEventDTO
    {
        [Parameter("address", "operator", 1, false )]
        public virtual string Operator { get; set; }
    }

    public partial class NewOracleEventDTO : NewOracleEventDTOBase { }

    [Event("NewOracle")]
    public class NewOracleEventDTOBase : IEventDTO
    {
        [Parameter("address", "oracle", 1, false )]
        public virtual string Oracle { get; set; }
    }

    public partial class NewOracleUpdateAllowanceEventDTO : NewOracleUpdateAllowanceEventDTOBase { }

    [Event("NewOracleUpdateAllowance")]
    public class NewOracleUpdateAllowanceEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "oracleUpdateAllowance", 1, false )]
        public virtual BigInteger OracleUpdateAllowance { get; set; }
    }

    public partial class NewTreasuryFeeEventDTO : NewTreasuryFeeEventDTOBase { }

    [Event("NewTreasuryFee")]
    public class NewTreasuryFeeEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "treasuryFee", 2, false )]
        public virtual BigInteger TreasuryFee { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true )]
        public virtual string NewOwner { get; set; }
    }

    public partial class PauseEventDTO : PauseEventDTOBase { }

    [Event("Pause")]
    public class PauseEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
    }

    public partial class PausedEventDTO : PausedEventDTOBase { }

    [Event("Paused")]
    public class PausedEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, false )]
        public virtual string Account { get; set; }
    }

    public partial class RewardsCalculatedEventDTO : RewardsCalculatedEventDTOBase { }

    [Event("RewardsCalculated")]
    public class RewardsCalculatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
        [Parameter("uint256", "rewardBaseCalAmount", 2, false )]
        public virtual BigInteger RewardBaseCalAmount { get; set; }
        [Parameter("uint256", "rewardAmount", 3, false )]
        public virtual BigInteger RewardAmount { get; set; }
        [Parameter("uint256", "treasuryAmount", 4, false )]
        public virtual BigInteger TreasuryAmount { get; set; }
    }

    public partial class StartRoundEventDTO : StartRoundEventDTOBase { }

    [Event("StartRound")]
    public class StartRoundEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
    }

    public partial class TokenRecoveryEventDTO : TokenRecoveryEventDTOBase { }

    [Event("TokenRecovery")]
    public class TokenRecoveryEventDTOBase : IEventDTO
    {
        [Parameter("address", "token", 1, true )]
        public virtual string Token { get; set; }
        [Parameter("uint256", "amount", 2, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class TreasuryClaimEventDTO : TreasuryClaimEventDTOBase { }

    [Event("TreasuryClaim")]
    public class TreasuryClaimEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "amount", 1, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class UnpauseEventDTO : UnpauseEventDTOBase { }

    [Event("Unpause")]
    public class UnpauseEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "epoch", 1, true )]
        public virtual BigInteger Epoch { get; set; }
    }

    public partial class UnpausedEventDTO : UnpausedEventDTOBase { }

    [Event("Unpaused")]
    public class UnpausedEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, false )]
        public virtual string Account { get; set; }
    }

    public partial class MAX_TREASURY_FEEOutputDTO : MAX_TREASURY_FEEOutputDTOBase { }

    [FunctionOutput]
    public class MAX_TREASURY_FEEOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class AdminAddressOutputDTO : AdminAddressOutputDTOBase { }

    [FunctionOutput]
    public class AdminAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }





    public partial class BufferSecondsOutputDTO : BufferSecondsOutputDTOBase { }

    [FunctionOutput]
    public class BufferSecondsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class ClaimableOutputDTO : ClaimableOutputDTOBase { }

    [FunctionOutput]
    public class ClaimableOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class CurrentEpochOutputDTO : CurrentEpochOutputDTOBase { }

    [FunctionOutput]
    public class CurrentEpochOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class GenesisLockOnceOutputDTO : GenesisLockOnceOutputDTOBase { }

    [FunctionOutput]
    public class GenesisLockOnceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }



    public partial class GenesisStartOnceOutputDTO : GenesisStartOnceOutputDTOBase { }

    [FunctionOutput]
    public class GenesisStartOnceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }



    public partial class GetUserRoundsOutputDTO : GetUserRoundsOutputDTOBase { }

    [FunctionOutput]
    public class GetUserRoundsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256[]", "", 1)]
        public virtual List<BigInteger> ReturnValue1 { get; set; }
        [Parameter("tuple[]", "", 2)]
        public virtual List<BetInfo> ReturnValue2 { get; set; }
        [Parameter("uint256", "", 3)]
        public virtual BigInteger ReturnValue3 { get; set; }
    }

    public partial class GetUserRoundsLengthOutputDTO : GetUserRoundsLengthOutputDTOBase { }

    [FunctionOutput]
    public class GetUserRoundsLengthOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class IntervalSecondsOutputDTO : IntervalSecondsOutputDTOBase { }

    [FunctionOutput]
    public class IntervalSecondsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
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

    public partial class MinBetAmountOutputDTO : MinBetAmountOutputDTOBase { }

    [FunctionOutput]
    public class MinBetAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OperatorAddressOutputDTO : OperatorAddressOutputDTOBase { }

    [FunctionOutput]
    public class OperatorAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class OracleOutputDTO : OracleOutputDTOBase { }

    [FunctionOutput]
    public class OracleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class OracleLatestRoundIdOutputDTO : OracleLatestRoundIdOutputDTOBase { }

    [FunctionOutput]
    public class OracleLatestRoundIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OracleUpdateAllowanceOutputDTO : OracleUpdateAllowanceOutputDTOBase { }

    [FunctionOutput]
    public class OracleUpdateAllowanceOutputDTOBase : IFunctionOutputDTO 
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



    public partial class PausedOutputDTO : PausedOutputDTOBase { }

    [FunctionOutput]
    public class PausedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }



    public partial class RefundableOutputDTO : RefundableOutputDTOBase { }

    [FunctionOutput]
    public class RefundableOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }



    public partial class RoundsOutputDTO : RoundsOutputDTOBase { }

    [FunctionOutput]
    public class RoundsOutputDTOBase : IFunctionOutputDTO 
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

















    public partial class TreasuryAmountOutputDTO : TreasuryAmountOutputDTOBase { }

    [FunctionOutput]
    public class TreasuryAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TreasuryFeeOutputDTO : TreasuryFeeOutputDTOBase { }

    [FunctionOutput]
    public class TreasuryFeeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class UserRoundsOutputDTO : UserRoundsOutputDTOBase { }

    [FunctionOutput]
    public class UserRoundsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
