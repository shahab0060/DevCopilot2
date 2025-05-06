namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class MathExtensions
    {

        public static double CalculatePercentage(this long value, long totalValue, int roundValue = 2)
        {
            double result = 0;
            if (value > 0)
            {
                result = (value * 100) / totalValue;
                result = Math.Round(result, roundValue);
            }
            return result;
        }

        public static string CalculatePercentageText(this long value, long totalValue, int roundValue = 2)
        => $"{value.CalculatePercentage(totalValue, roundValue)}%";

        public static int GetDiscount(this int price, int percentage)
        {
            int discount = (int)(price * (percentage / 100.0));
            return (discount / 1000) * 1000;
        }


    }
}
