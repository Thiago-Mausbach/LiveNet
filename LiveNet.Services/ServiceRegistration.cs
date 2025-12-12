using LiveNet.Services.Interfaces;
using LiveNet.Services.Services;
using Microsoft.Extensions.DependencyInjection;



namespace LiveNet.Services;

public static class ServiceRegistration
{
    public static IServiceCollection AddLiveNetServices(this IServiceCollection services)
    {
        //  -------Builder para testes locais---------- -
        //services.AddDbContext<AppDbContext>(options =>
        //options.UseInMemoryDatabase(("DefaultConnection")));

        services.AddScoped<IContatoService, ContatoService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IServicoService, ServicoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioAtualService, UsuarioAtualService>();

        services.AddHttpContextAccessor();

        return services;
    }
}
