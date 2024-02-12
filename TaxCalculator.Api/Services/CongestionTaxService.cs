using TaxCalculator.Api.Configuration;
using TaxCalculator.Api.Factories;
using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.Api.Services
{
	public class CongestionTaxService : ICongestionTaxService
    {
        public int Calculate(VehicleBase vehicle, string[] dates)
        {

            var cityTaxService = CityTaxFactory.CreateCityTaxService(AppConfiguration.DefaultCity);

            var taxResult = cityTaxService.GetCongestionTax(vehicle, dates.Select(d => DateTime.Parse(d)).ToArray());

            return taxResult;
        }
    }
}

