using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDev.Ioc.MsExtDi.Testing.Example
{
    public class FoobarInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IFoobar, Foobar>();

            services.ConfigureSettings<FoobarSettings>(configuration);
        }
    }
}
