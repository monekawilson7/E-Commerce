using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    { 
        return Ok(new Product {Id = id});
    }
    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok(new Product { });
    }

    [HttpPost]
    public ActionResult Create(Product product)
    {
        //Results.Created("Test",product);
        return Created("Test",product);
    }

    [HttpPut]
    public ActionResult Update(Product product)
    {
        return Ok( product);
    }

    [HttpDelete]
    public ActionResult Delete(int id)
    {
        return NoContent();
    }

    public class Product
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Test";
    }
}
