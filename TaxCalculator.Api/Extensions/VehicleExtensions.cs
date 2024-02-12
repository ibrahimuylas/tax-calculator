using System;
using TaxCalculator.Data.Constants;
using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.Api.Extensions
{
    public static class VehicleExtensions
    {
        public static bool IsTollFreeVehicle(this VehicleBase vehicle)
        {
            return VehiclesConstants.TollFreeVehicles.Any(v => v.Equals(vehicle.Type, StringComparison.OrdinalIgnoreCase));
        }
    }
}

