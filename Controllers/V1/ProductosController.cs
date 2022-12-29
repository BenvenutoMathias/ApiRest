using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestApi.DTO.V1;
using RestApi.DTO.V2;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace RestApi.Controllers.V1
{
    [ApiVersion("1.0")]
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

            Product[]? Product = JsonSerializer.Deserialize<Product[]>(responseBody);

            return Ok(Product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            Stream response = await _httpClient.GetStreamAsync("https://fakestoreapi.com/products/1");

            Product? Product = JsonSerializer.Deserialize<Product>(response);

            return Ok(Product);
        }
    }
}
