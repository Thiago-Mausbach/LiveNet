using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Services.Interfaces;
using LiveNet.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace LiveNet.Services;

public static class ServiceRegistration
{
    public static IServiceCollection AddLiveNetServices(this IServiceCollection services)
    {
        //-------Builder para testes locais---------- -
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase(("DefaultConnection")));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IContatoService, ContatoService>();
        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IFavoritoService, FavoritoService>();
        services.AddScoped<IInteresseService, InteresseService>();
        services.AddScoped<IServicoService, ServicoService>();
        services.AddScoped<IUsuarioAtualService, UsuarioAtualService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPasswordHasher<UsuarioModel>, PasswordHasher<UsuarioModel>>();


        services.AddHttpContextAccessor();

        return services;
    }


}
