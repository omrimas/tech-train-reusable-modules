using System.Collections.Generic;
using System.Threading.Channels;
using Shared.Validators;

namespace Shared.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApiValidator(this IServiceCollection services)
        {
            services.AddSingleton(Channel.CreateUnbounded<string>(new UnboundedChannelOptions() { SingleReader = true }));
            services.AddSingleton(svc => svc.GetRequiredService<Channel<string>>().Reader);
            services.AddSingleton(svc => svc.GetRequiredService<Channel<string>>().Writer);
            services.AddHostedService<ApiValidationErrorsListener>();

            services.AddSingleton<IApiValidator, ApiDescriptionValidator>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IApiValidators apiValidators = new ApiValidators(serviceProvider.GetServices<IApiValidator>());

            services.AddSingleton<IApiValidators, ApiValidators>();

            return services;
        }
    }
}
