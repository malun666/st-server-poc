using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST.Server.Models.Products;

namespace ST.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private static readonly List<Product> _products = new();

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            Id = _products.Count + 1,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };

        _products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateProductDto dto)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        _products.Remove(product);
        return NoContent();
    }

    [HttpPost("batch")]
    public ActionResult<IEnumerable<Product>> CreateBatch([FromBody] IEnumerable<CreateProductDto> dtos)
    {
        var products = new List<Product>();
        foreach (var dto in dtos)
        {
            var product = new Product
            {
                Id = _products.Count + 1,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };
            _products.Add(product);
            products.Add(product);
        }
        
        return Ok(products);
    }
} 