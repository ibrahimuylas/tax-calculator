using TaxCalculator.Api.Services;

namespace TaxCalculator.Api.Factories
{
	public static class CityTaxFactory
	{
        public static ICityTaxService CreateCityTaxService(string cityName)
        {
            return cityName.ToLower() switch
            {
                _ => new GothenburgTaxService(),
            };
        }

	}
}

