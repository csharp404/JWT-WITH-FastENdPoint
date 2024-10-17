using Api.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.FastEndPoints
{
    public sealed record DeleteRequest([FromQuery]string id);
    public sealed record DeleteResponse(bool flag);


    public class DeleteStudent(UserManager<AppUser> userManager):Endpoint<DeleteRequest,DeleteResponse>
    {
        public override void Configure()
        {
            Delete("/Student/");
            AllowAnonymous();
        }

        public override Task HandleAsync(DeleteRequest req, CancellationToken ct)
        {
            var result = userManager.FindByIdAsync(req.id).Result;
            var res = userManager.DeleteAsync(result).Result;
            return SendAsync(new DeleteResponse(true));
        }
    }
}
