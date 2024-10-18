using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerticalSlicingApi.Data;
using VerticalSlicingApi.Domain;
using VerticalSlicingApi.DTOs;

namespace VerticalSlicingApi.Service
{
    public class ProductService(AppDbContext context)
    {
        public async Task<List<Product>> GetAll()
        {
            return context.Products.ToListAsync().Result;
        } 
        public async Task Create(ProductDTO product)
        {
            var data = product.Adapt<Product>();
            await context.Products.AddAsync(data);
            context.SaveChanges();
        } 
        public async Task Update(Product product)
        {
            var data = context.Products.Find(product.Id);
           /* data.Adapt(product);*/
            data.Name = product.Name;
            data.Description = product.Description;
            data.Price = product.Price;
            context.SaveChanges();

        }
        public async Task Delete(int id)
        {
            var data = context.Products.Find(id);
            if(data!=null)
            {
                context.Products.Remove(data);
               await context.SaveChangesAsync();
            }
        }
        public async Task<Product> GetById(int id)
        {
            var data = context.Products.Find(id);
            if (data != null)
            {
                return data;
            }

            return new Product();
        }
    }
}
