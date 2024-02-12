using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.Api.Services
{
    public interface ICongestionTaxService
    {
        int Calculate(VehicleBase vehicle, string[] dates);
    }
}