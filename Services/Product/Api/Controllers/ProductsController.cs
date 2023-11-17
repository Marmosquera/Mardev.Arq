using AutoMapper;
using Mardev.Arq.Services.Product.Contracts;
using Mardev.Arq.Services.Product.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Mardev.Arq.Services.Product.Api.Controllers
{
    [ApiController]
    [SwaggerTag(description: "Browse/manage the products")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;


        private List<ProductDto> _products = new List<ProductDto>() {
            new ProductDto
            {
                ProductId = 1,
                Name = "Samosa",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Appetizer"
            },
            new ProductDto
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/602x402",
                CategoryName = "Appetizer"
            },
            new ProductDto
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/601x401",
                CategoryName = "Dessert"
            },
            new ProductDto
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/600x400",
                CategoryName = "Entree"
            }};

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Get products",
            Description = "Return an empty list or a list with all the products"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "list of products", typeof(ProductGetResponse))]
        public IActionResult Get()
        {
            var response = new ProductGetResponse { Items = _products };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductGetByIdResponse> GetById(int id)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<ProductGetByIdResponse>(product);
            return response;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
