using System.Text.Json;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Components.Forms;
using StackExchange.Redis;

namespace Api.Caching
{
    public class RedisCaching(IConnectionMultiplexer Redis, AppDbContext context )
    {
        public List<School> GetAll()
        {
            var db = Redis.GetDatabase();
            var value = db.StringGet("School");
            if (!value.IsNullOrEmpty)
            {
                var data = JsonSerializer.Deserialize<List<School>>(value);
                Console.WriteLine("From Redis");
                return data;
            }

            var dta = context.Schools.ToList();
            var val = JsonSerializer.Serialize(dta);
            db.StringSet("School", val,TimeSpan.FromSeconds(5));
            Console.WriteLine("From Db");
            return dta;


        }
    }
}
