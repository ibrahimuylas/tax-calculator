﻿using TaxCalculator.Api.Extensions;
using TaxCalculator.Data.Models.Cities;
using TaxCalculator.Data.Models.Vehicles;

namespace TaxCalculator.Api.Services
{
	public class GothenburgTaxService : ICityTaxService
    {

        public CityBase City { get; set; }

        public GothenburgTaxService()
        {
            City = new Gothenburg();
        }

        /**
        * Calculate the total toll fee for one day
        *
        * @param vehicle - the vehicle
        * @param dates   - date and time of all passes on one day
        * @return - the total congestion tax for that day
        */

        public int GetCongestionTax(VehicleBase vehicle, DateTime[] dates)
        {
            var orderedDates = dates.OrderBy(d => d).ToArray();
            DateTime intervalStart = orderedDates[0];
            int totalFee = 0;

            foreach (DateTime date in orderedDates)
            {
                int nextFee = GetCongestionTollFee(vehicle, date);
                int tempFee = GetCongestionTollFee(vehicle, intervalStart);

                TimeSpan diffInMillies = date - intervalStart;
                var minutes = diffInMillies.TotalMinutes;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }

                intervalStart = date;
            }

            if (totalFee > 60)
            {
                totalFee = 60;
            }

            return totalFee;
        }

        public int GetCongestionTollFee(VehicleBase vehicle, DateTime date)
        {
            if (this.IsTollFreeDate(date) ||
                vehicle.IsTollFreeVehicle())
                return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        public bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
            {
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }

    }
}

