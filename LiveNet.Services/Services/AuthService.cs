using LiveNet.Database.Context;
using LiveNet.Domain;
using LiveNet.Domain.Models;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiveNet.Services.Services;

public class AuthService(ApplicationDbContext context, IConfiguration configuration) : IAuthService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;
    private readonly PasswordHasher<UsuarioModel> _passwordHasher = new();

    public async Task<AuthResult> LoginAsync(LoginViewModel model)
    {
        UsuarioModel? user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Senha, model.Senha) != PasswordVerificationResult.Success)
            return new AuthResult { Sucesso = false, Mensagem = "Email ou senha incorretos" };


        var claims = new[]
           {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
    };

        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthResult
        {
            Sucesso = true,
            Token = tokenHandler.WriteToken(token)
        };
    }
}
