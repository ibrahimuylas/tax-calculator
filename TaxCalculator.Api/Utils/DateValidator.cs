namespace TaxCalculator.Api.Utils
{
	public class DateValidator
	{
        public static bool AreAllDatesValid(string[] dates)
        {
            if (dates == null || dates.Length == 0)
                return false;

            foreach (string date in dates)
            {
                if (!DateTime.TryParse(date, out _))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

