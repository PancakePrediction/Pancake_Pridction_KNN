namespace StockIndicatorLib
{
    public class IndicatorPeriod
    {
        private int _ParamIndex = -1;
        private string _ParamName = "";
        private double _ParamValue = 0.0;

        public IndicatorPeriod(int index, string name, double value)
        {
            this._ParamIndex = index;
            this._ParamName = name;
            this._ParamValue = value;
        }

        public int ParamIndex
        {
            get
            {
                return this._ParamIndex;
            }
            set
            {
                this._ParamIndex = value;
            }
        }

        public string ParamName
        {
            get
            {
                return this._ParamName;
            }
            set
            {
                this._ParamName = value;
            }
        }

        public double ParamValue
        {
            get
            {
                return this._ParamValue;
            }
            set
            {
                this._ParamValue = value;
            }
        }
    }
}


