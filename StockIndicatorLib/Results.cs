using System;
using System.Collections.Generic;
using System.Text;

namespace StockIndicatorLib
{

    public abstract class BaseIndicatorResult
    {
        public BaseIndicator indicator
        {
            get;
        }
        protected BaseIndicatorResult(BaseIndicator indicator)
        {
            this.indicator = indicator;
        }
    }
}
