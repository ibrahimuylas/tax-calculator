using TaxCalculator.Data.Models.Cities;
using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.Api.Services
{
	public interface ICityTaxService
    {

        CityBase City { get; protected set; }

        int GetCongestionTax(VehicleBase vehicle, DateTime[] dates);

        int GetCongestionTollFee(VehicleBase vehicle, DateTime date);

        bool IsTollFreeDate(DateTime date);

    }
}

