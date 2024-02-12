using System;
namespace TaxCalculator.Data.Constants
{
    public class VehiclesConstants
    {
        public const string Car = "Car";
        public const string Motorcycle = "Motorcycle";
        public const string Tractor = "Tractor";
        public const string Emergency = "Emergency";
        public const string Diplomat = "Diplomat";
        public const string Foreign = "Foreign";
        public const string Military = "Military";

        public static readonly List<string> TollFreeVehicles = new() {
            Motorcycle, Tractor, Emergency, Diplomat, Foreign, Military
        };
    }
}

