using Accord.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Accord_KNN
{
    public class KNNResult
    {
        public int RoundID;
        public bool PredictionIsBull;
        public bool PredictionIsBull2;
        public bool PredictionIsBullV2;
        public bool PredictionIsBullV3_0123;
        public bool PredictionIsBullV1_0123;
        public double Score;
        public double TestSetAccuracy;
        public int kValue;
        public double AgainstPercent;
        internal KNearestNeighbors knn;
    }

    public class BestParam
    {
        public int kValue;
        public int testSetCount;
        public double winRatio;
        public int serialLost;
        public int totalTestCount;
    }
}
