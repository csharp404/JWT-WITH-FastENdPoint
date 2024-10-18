using Api.Data;
using Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Caching
{
    public class MemoryCaching (AppDbContext context,IMemoryCache Cache)
    {

        public List<School> GetAll()
        {
            if (Cache.TryGetValue("School", out List<School> sch))
            {
                Console.WriteLine("From Memory");

                return sch;

            }

            var data = context.Schools.ToList();
            var option = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            };
            Cache.Set("School", data,option);
            Console.WriteLine("From DataBase");
            return data;
        }
     

       

      
    }
}
