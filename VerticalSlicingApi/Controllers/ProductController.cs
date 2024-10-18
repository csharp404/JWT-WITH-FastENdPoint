using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalSlicingApi.Domain;
using VerticalSlicingApi.DTOs;
using VerticalSlicingApi.Feature.Create;
using VerticalSlicingApi.Feature.Delete;
using VerticalSlicingApi.Feature.GetAll;
using VerticalSlicingApi.Feature.GetById;
using VerticalSlicingApi.Feature.Update;

namespace VerticalSlicingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController (ISender sender): ControllerBase
    {
        [HttpGet]
        [Route("Get")]
        public async Task<List<Product>> Get()
        {
           GetAllRequestQuery query = new GetAllRequestQuery();
           var data = await sender.Send(query);
           return data;
        }
        [HttpGet]
        [Route("GetById/{id:Yousef}")]
        public async Task<Product> GetById(int id)
        {
            GetByIdRequestQuery query = new (id);
            var data = await sender.Send(query);
            return data;

        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                DeleteRequestCommand command = new(id);
                sender.Send(command);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(Product product)
        {
            try
            {
                UpdateRequestCommand command = new(product);
                sender.Send(command);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(ProductDTO product)
        {
            try
            {
                CreateRequestCommand command = new CreateRequestCommand(product);
                sender.Send(command);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
