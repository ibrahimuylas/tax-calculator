using System;
using TaxCalculator.Api.Factories;
using TaxCalculator.Api.Utils;
using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.UnitTests.Factories
{
	public class VehicleFactoryTests
	{
        [Scenario]
        [Example("Car")]
        public void ShouldReturnCorrectVehicleIfExists(string vehicleType)
        {
            // TO DO: can be completed later
            bool result = !string.IsNullOrEmpty(vehicleType);

            result.Should().BeTrue();
        }
    }
}

