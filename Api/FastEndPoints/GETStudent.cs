using Api.Data;
using Api.Models;
using FastEndpoints;

namespace Api.FastEndPoints
{

    public sealed record StudentResponse(List<AppUser> Students);
    public class GETStudent(AppDbContext db) : EndpointWithoutRequest<StudentResponse>
    {
        public override void Configure()
        {
            Get("/Student/");
            AllowAnonymous();
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            var data = db.Users.ToList();
            var res = new StudentResponse(data);
            return SendAsync(res,200,ct);
        }
    }

}
