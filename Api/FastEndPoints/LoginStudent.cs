using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

namespace Api.FastEndPoints
{
    public sealed record LoginRequest(string email, string password);
    public sealed record LoginResponse(string jwt);

        public class  LoginRequestValidator : Validator<LoginRequest>
        {
            public LoginRequestValidator()
            {
                RuleFor(x => x.email).NotEmpty().NotNull();
                RuleFor(x => x.password).NotEmpty().NotNull();
            }
        }
    public class LoginStudent(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,IConfiguration configuration):Endpoint<LoginRequest,LoginResponse>
    {
        public override void Configure()
        {
           Get("/Login/");
           AllowAnonymous();
        }
        public override Task HandleAsync(LoginRequest req, CancellationToken ct)
        {

            var usr = userManager.FindByEmailAsync(req.email).Result;
            var result = signInManager.CheckPasswordSignInAsync(usr, req.password, true).Result;
            if (result.Succeeded)
            {
                var response = new LoginResponse(GenerateJWT(usr));
                return SendAsync(response);
            }
            var respons = new LoginResponse("");
            return SendAsync(respons);
        }

        private string GenerateJWT(AppUser usr)
        {
            var jwt = Encoding.ASCII.GetBytes(configuration.GetSection("JWTSECERT").Value);
            var claims = new List<Claim>()
            {
                new Claim("Id", usr.Id),
                new Claim("Email", usr.Email)
            };

            var js = new SecurityTokenDescriptor()
            {
                Audience = "US",
                Issuer = "US",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwt),
                    SecurityAlgorithms.HmacSha256)

            };
            var token = new JwtSecurityTokenHandler();
            var tok = token.CreateToken(js);
            return token.WriteToken(tok);


          
        }
    }
}
