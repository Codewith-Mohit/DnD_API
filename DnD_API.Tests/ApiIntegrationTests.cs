using DnD_API;
using DnD_API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;

namespace DnD_API.Tests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Dice_roll_endpoint_returns_result()
        {
            var resp = await _client.PostAsJsonAsync("/dice/roll", new { Formula = "2d6+1", seed= 12 }, cancellationToken: TestContext.Current.CancellationToken);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadFromJsonAsync<DiceRollResult>(cancellationToken: TestContext.Current.CancellationToken);
            Assert.NotNull(json);
            System.Diagnostics.Debug.WriteLine(json);
            Assert.Equal("2d6+1", json.Formula);
        }
    }
}