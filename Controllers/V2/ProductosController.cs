using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTO.V2;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestApi.Controllers.V2
{

    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private const string ApiTestUrl = "https://dummyapi.io/data/v1/post";
        private readonly HttpClient _httpClient;

        public ProductosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://fakestoreapi.com/products");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            ProductV2[]? Product = JsonSerializer.Deserialize<ProductV2[]>(responseBody);

            return Ok(Product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            Stream response = await _httpClient.GetStreamAsync($"https://fakestoreapi.com/products/{id}");

            ProductV2? Product = JsonSerializer.Deserialize<ProductV2>(response);

            return Ok(Product);
        }
    }
}
