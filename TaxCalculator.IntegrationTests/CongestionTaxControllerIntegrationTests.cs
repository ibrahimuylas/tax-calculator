using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TaxCalculator.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TaxCalculator.IntegrationTests;

public class CongestionTaxControllerIntegrationTests : IDisposable
{
    private readonly IWebHost _host;
    private readonly HttpClient _client;

    /// <summary>
    /// There is an issue on hosting app for integration tests. I couldn't have enough time to review and fix it. 
    /// </summary>
    public CongestionTaxControllerIntegrationTests()
    {
        // Build and start the web host
        _host = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(services =>
            {
                services.AddControllers();
                services.AddScoped<ICongestionTaxService, CongestionTaxService>();


                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Tax Calculator API",
                        Description = "An ASP.NET Core Web API for managing tax calculations for vehicles",
                    });

                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
            })
            .Configure(app =>
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            })
            .Start();

        // Create HttpClient
        _client = _host.GetTestClient();
    }

    [Theory]
    [InlineData("Car", new string[] { "2013-02-07 07:27:00", "2013-02-07 08:29:00", "2013-02-07 11:29:00" })]
    public async Task CalculateEndpoint_ReturnsExpectedResult(string vehicleType, string[] dates)
    {
        // Arrange
        var request = new
        {
            VehicleType = vehicleType,
            Dates = dates
        };
        var jsonRequest = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/CongestionTax", httpContent);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Deserialize response body
        var responseBody = await response.Content.ReadAsStringAsync();
        var taxResult = JsonSerializer.Deserialize<int>(responseBody);

        Assert.True(taxResult >= 0);
    }

    public void Dispose()
    {
        // Dispose the web host and client
        _host.Dispose();
        _client.Dispose();
    }
}
