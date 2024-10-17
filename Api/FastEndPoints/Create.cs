using Api.Data;
using Api.DTOs;
using Api.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Api.FastEndPoints
{
    public sealed record CreateRequest(RegisterDTO usr);
    public sealed record CreateResponse(bool flag);

    public class Create(UserManager<AppUser> usermanager):Endpoint<CreateRequest, CreateResponse>
    {
        public override void Configure()
        {
            Post("/Student/");
            AllowAnonymous();
        }

        public override  Task HandleAsync(CreateRequest req, CancellationToken ct)
        {
            var usrr = new AppUser()
            {
                UserName = req.usr.UserName,
                Email = req.usr.Email,
                PhoneNumber = req.usr.PhoneNumber,
                age = req.usr.Age,
                Schoolid = 1
                
            };
           var respo =   usermanager.CreateAsync(usrr, req.usr.Password).Result;

            var res = new CreateResponse(true);
            return SendAsync(res);
        }
    }
}
