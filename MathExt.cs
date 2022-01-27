namespace Pancake_Pridction_KNN
{
    public static class MathExt
    {
        public static bool Between(this double value, double min, double max)
        {
            return value >= min && value < max;
        }

        public static double Percent(this float a, double b)
        {
            return (a - b) / b * 100;
        }

        public static double GrowUp(this double a, double b)
        {
            return (b - a) / a * 100;
        }

        public static double Percent(this double a, double b)
        {
            return (a - b) / b * 100;
        }
        public static float Percent(this float a, float b)
        {
            return (a - b) / b * 100;
        }

    }


}
