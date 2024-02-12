using TaxCalculator.Data.Constants;
using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.Api.Factories
{
	public static class VehicleFactory
	{
        public static VehicleBase? GetVehicle(string vehicleType)
        {
            return vehicleType switch
            {
                string vType when VehiclesConstants.Motorcycle.Equals(vType, StringComparison.OrdinalIgnoreCase) => new Motorbike(),
                string vType when VehiclesConstants.Car.Equals(vType, StringComparison.OrdinalIgnoreCase) => new Car(),
                _ => null,
            };
        }
    }
}

