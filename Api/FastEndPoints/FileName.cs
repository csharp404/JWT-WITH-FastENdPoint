using Api.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.FastEndPoints
{
    public sealed record UpdateRequest( int age, [FromQuery] string id);
    public sealed record UpdateResponse(bool flag);

    public class FileName(UserManager<AppUser> userManager):Endpoint<UpdateRequest,UpdateResponse>
    {
        public override void Configure()
        {
           Put("/Student/");
           AllowAnonymous();
        }

        public override Task HandleAsync(UpdateRequest req, CancellationToken ct)
        {
            try
            {
                var usr = userManager.FindByIdAsync(req.id).Result;
                usr.age = req.age;
               var result =  userManager.UpdateAsync(usr).Result;
                var res = new UpdateResponse(true);
                return SendAsync(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
