using System;

namespace WebCore.Utils.Helpers
{
    public static class Extensions
    {
        public static string ToDateString(this DateTime? date)
        {
            if (date == null)
            {
                return string.Empty;
            }
            return ToDateString(date.Value);
        }

        public static string ToDateString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string ToCurrencyString(this decimal money, string moneyType = "VND")
        {
            if (moneyType == "VND")
            {
                return money.ToString("0,000");
            }
            return money.ToString("0,000.00");
        }

        public static string ToCurrencyString(this decimal? money, string moneyType = "VND")
        {
            return ToCurrencyString(money.GetValueOrDefault(0), moneyType);
        }
    }

}
