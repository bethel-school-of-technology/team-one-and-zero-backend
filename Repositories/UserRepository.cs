using TEAM_ONE_ZERO_BACKEND.Models;
using TEAM_ONE_ZERO_BACKEND.Migrations;
using bycrypt = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TEAM_ONE_ZERO_BACKEND.Repositories;

public class UserRepository : IUserRepository
{
    
    private static PoPDbContext _context;
    private static IConfiguration _config;
    public UserRepository(PoPDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }
    private string BuildToken(User user) {
        var secret = _config.GetValue<string>("TokenSecret");
        var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        // Create signiture using secret key
        var signingCredentials = new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256);
        
        //Create Claims to add JWT
        varr claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.Name, user.Username ?? ""),
        };

        //Create Token
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials);

        // Encode token
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }
    public User CreateUser(User user)
    {
        var passwordHash = bycrypt.HashPassword(user.Password);
        user.password = passwordHash;

        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == username);
        var verified = false;
        
        if (user != null) {
            verified = bycrypt.Verify(password, user.password);
        }

        if (user == null || !verified) 
        {
            return string.Empty;
        }

        // Create & Return JWT
    return BuildToken(user);
    }
}