using System;
using System.Net;
using CongestionTaxCalculator.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxCalculator.Api.DTO;
using TaxCalculator.Api.Services;

namespace TaxCalculator.UnitTests.Controllers
{
	public class CongestionTaxControllerTests
	{
        private readonly Mock<ILogger<CongestionTaxController>> _loggerMock;
        private readonly Mock<ICongestionTaxService> _congestionTaxServiceMock;
        private readonly CongestionTaxController _congestionTaxController;

        public CongestionTaxControllerTests()
		{
            _loggerMock = new Mock<ILogger<CongestionTaxController>>();
            _congestionTaxServiceMock = new Mock<ICongestionTaxService>();

            _congestionTaxController = new CongestionTaxController(_congestionTaxServiceMock.Object, _loggerMock.Object);

        }

        [Scenario]
        [Example(null)]
        [Example("")]
        [Example("   ")]
        [Example("Caar")]
        [Example("Bus")]
        public void ShouldReturnBadRequestObjectResultWhenVehicleTypeIsInvalid(string vehicleType)
        {
            var invalidDTO = new CongestionTaxRequestDTO();
            IActionResult? response = null;

            _ = "Given a invalid VehicleType is received for congestion tax calculation".x(() =>
            {
                invalidDTO = new CongestionTaxRequestDTO
                {
                    Dates = new string[] { "2013-02-07 07:27:00", "2013-02-07 09:27:00" },
                    VehicleType = vehicleType
                };
            });
            "When the the request is handled".x( () =>
            {
                response = _congestionTaxController.Calculate(invalidDTO);
            });
            "Then return BadRequest".x(() =>
            {
                var apiResponse = response.As<BadRequestObjectResult>();
                apiResponse.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
                apiResponse.Value.Should().Be($"Vehicle {invalidDTO.VehicleType} does not found!");
            });
        }

        [Scenario]
        [Example(new object[] { new string[] { "" } })]
        [Example(new object[] { new string[] { "01/01/2015 aa:hh" } })]
        [Example(new object[] { new string[] { "2013-02-07 15:37:00", "date_date" } })]
        public void ShouldReturnBadRequestObjectResultWhenDatesInvalid(string[] dates)
        {
            var invalidDTO = new CongestionTaxRequestDTO();
            IActionResult? response = null;

            _ = "Given a invalid VehicleType is received for congestion tax calculation".x(() =>
            {
                invalidDTO = new CongestionTaxRequestDTO
                {
                    Dates = dates,
                    VehicleType = "Car"
                };
            });
            "When the the request is handled".x(() =>
            {
                response = _congestionTaxController.Calculate(invalidDTO);
            });
            "Then return BadRequest".x(() =>
            {
                var apiResponse = response.As<BadRequestObjectResult>();
                apiResponse.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
                apiResponse.Value.Should().Be($"Invalid date entered. Please check dates!");
            });
        }

        [Scenario]
        [Example(new object[] { new string[] { } })]
        [Example(new object[] { })]
        [Example(null)]
        public void ShouldReturnBadRequestObjectResultWhenDatesEmpty(string[] dates)    
        {
            var invalidDTO = new CongestionTaxRequestDTO();
            IActionResult? response = null;

            _ = "Given a invalid VehicleType is received for congestion tax calculation".x(() =>
            {
                invalidDTO = new CongestionTaxRequestDTO
                {
                    Dates = dates,
                    VehicleType = "Car"
                };
            });
            "When the the request is handled".x(() =>
            {
                response = _congestionTaxController.Calculate(invalidDTO);
            });
            "Then return BadRequest".x(() =>
            {
                var apiResponse = response.As<BadRequestObjectResult>();
                apiResponse.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
                apiResponse.Value.Should().Be($"The Dates field is required.");
            });
        }
    }
}

