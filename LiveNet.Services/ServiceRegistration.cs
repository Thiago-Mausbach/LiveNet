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

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IContatoService, ContatoService>();
        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IFavoritoService, FavoritoService>();
        services.AddScoped<IServicoService, ServicoService>();
        services.AddScoped<IUsuarioAtualService, UsuarioAtualService>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        services.AddHttpContextAccessor();

        return services;
    }
}
