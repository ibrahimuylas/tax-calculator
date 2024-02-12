using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Api.DTO;
using TaxCalculator.Api.Factories;
using TaxCalculator.Api.Services;
using TaxCalculator.Api.Utils;

namespace CongestionTaxCalculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CongestionTaxController : ControllerBase
{
    private readonly ILogger<CongestionTaxController> _logger;
    private readonly ICongestionTaxService _congestionTaxService;

    public CongestionTaxController(ICongestionTaxService congestionTaxService, ILogger<CongestionTaxController> logger)
    {
        _logger = logger;
        _congestionTaxService = congestionTaxService;
    }

    /// <summary>Congestion tax calculation for vehicles</summary>
    /// <remarks>
    /// **Sample request body:**
    /// 
    ///     {
    ///         "vehicleType": "Car",
    ///         "dates": [
    ///             "2013-02-07 07:27:00",
    ///             "2013-02-07 08:29:00",
    ///             "2013-02-07 11:29:00",
    ///             "2013-02-07 15:37:00",
    ///             "2013-02-07 15:27:00",
    ///             "2013-02-07 17:27:00",
    ///             "2013-02-07 18:27:00"
    ///         ]
    ///     }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Calculate([FromBody] CongestionTaxRequestDTO dto)
    {
        try
        {
            var vehicle = VehicleFactory.GetVehicle(dto.VehicleType);

            if (vehicle == null)
            {
                return new BadRequestObjectResult($"Vehicle {dto.VehicleType} does not found!");
            }

            if (dto.Dates == null || dto.Dates.Length == 0)
            {
                return new BadRequestObjectResult($"The Dates field is required.");
            }

            if (!DateValidator.AreAllDatesValid(dto.Dates))
            {
                return new BadRequestObjectResult($"Invalid date entered. Please check dates!");
            }

            var taxResult = _congestionTaxService.Calculate(vehicle, dto.Dates);

            return new OkObjectResult(taxResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error happened for vehicleType: {dto.VehicleType}", ex);

            return new BadRequestObjectResult("Unexpected error happened! Please contact your administrator.");
        }
    }
}

