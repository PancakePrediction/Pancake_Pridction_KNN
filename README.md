![image](https://raw.githubusercontent.com/PancakePrediction/Pancake_Pridction_KNN/main/imgs/001.png)

# Pancake_Pridction_KNN
For Pancake Pridction, Try to use the machine learning algorithm KNN to predict it.

## Features
1. You can follow the chart to quick bets.
2. If you win the round, it will auto claim prize.
3. If you lost the round, you can click the button `+double` to bet next round.
4. You can collect more samples for the KNN, and Enable autobet to autoplay the prediction game.

## Donations
This is a free project but any funding is appricated.
ETH/BNB: 0x788c9F5406983Efe4f837d77CD7394Aca00Cb313

## How to Enable KNN and collect samples automaticaly
1. Create a MSSQL DB named `StockData`.
2. Create a `RoundData` table in DB `StockData` with code bellow:
```
USE [StockData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RoundData](
	[RoundID] [int] NOT NULL,
	[Last10sChange] [float] NULL,
	[LinkPriceSecounds] [int] NULL,
	[Offset] [float] NULL,
	[KDChange] [float] NULL,
	[Trend] [float] NULL,
	[KDRatio2] [float] NULL,
	[KDRatio3] [float] NULL,
	[Change] [float] NULL,
	[UpShadowLine] [float] NULL,
	[DownShadowLine] [float] NULL,
	[OnBollMiddle] [float] NULL,
	[IsBull] [bit] NULL,
	[ChangeBefore] [float] NULL,
	[K2] [float] NULL,
	[D2] [float] NULL,
	[K3] [float] NULL,
	[D3] [float] NULL,
	[BollWidth] [float] NULL,
	[BollChange] [float] NULL,
	[Change10m] [float] NULL,
	[Change15m] [float] NULL,
	[Change20m] [float] NULL,
	[Change25m] [float] NULL,
 CONSTRAINT [PK_RoundData1] PRIMARY KEY CLUSTERED 
(
	[RoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
```

3. Create a `RoundData_Prediction` table in DB `StockData` with code bellow:
```
USE [StockData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RoundData_Prediction](
	[RoundID] [int] NOT NULL,
	[PredictionIsBull] [bit] NULL,
	[PredictionIsBull2] [bit] NULL,
	[PredictionIsBullV2] [bit] NULL,
	[PredictionIsBullV3_0123] [bit] NULL,
	[PredictionIsBullV1_0123] [bit] NULL,
	[Score] [float] NULL,
	[TestSetAccuracy] [float] NULL,
	[kValue] [int] NULL,
	[AgainstPercent] [float] NULL,
 CONSTRAINT [PK_RoundData_Prediction1] PRIMARY KEY CLUSTERED 
(
	[RoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
```
4. Fill your MSSQL connection info to config files.


## Socials
Telegram: https://t.me/PancakePredictionKnn

## Config
```
The Config is listed in `Config\AppConfig.json` like bellow:
{
  "faildCount": 7,							//Log for faild count.
  "betAmountInBNB": 0.1,					//init amount to bet in BNB.
  "AutoBet": false,							//Enable auto bet
  "Proxy": "http://127.0.0.1:7890",			//It can set to ""
  "DbServerName": "127.0.0.1,1433",			//If will enable KNN if you set this value, default is null or empty("")
  "dbName": "StockData",					//Db name for KNN, must be "StockData"
  "dbUser": "stockdataUser",				//Your db user
  "dbPassword": "stockdataUserPassword",	//Your db user's password
  "websocketNode": "wss://speedy-nodes-nyc.moralis.io/cde6a7978ca113c11c427bc5/bsc/mainnet/ws",		//If you want to use WS then set it otherwise leave it
  "wallletPrivateKey": "0x118e6050ccb1d2c03dbe71e340be74f4371a639afe6f0d1bbb9d288fdcf639d2",		//Your walllet private key
  "rpc_Endpoint": "https://bsc-dataseed.binance.org/"												//You can change it if you have better node
}
```

### Recommand Bsc Node and Http Api
You can get a better node for quick bet and lessing data delay.
https://ankr.com/
https://www.quicknode.com/


## Running the project
If you want to run the project you can go to [releases](https://github.com/PancakePrediction/Pancake_Pridction_KNN/releases) and a binary that will execute on your OS.

## About KNN 
1. the K value 26 will be a good one.(Success 61.33%; Max Serial lost: 5)
2. More samples will make its predictions more accurate.

## Warrning
1. It's a gambling-like game, if you can't control your greed, you'll go broke.
2. I went from 0.5 BNB to 7 BNB in 1 day through this game. 
3. But I went from 70 BNB to 0.01 BNB in half day.