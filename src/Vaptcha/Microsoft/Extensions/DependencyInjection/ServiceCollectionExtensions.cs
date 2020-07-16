using System;
using System.Net.Http;
using iBestRead.Vaptcha;
using iBestRead.Vaptcha.Consts;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVaptcha(
            this IServiceCollection services, 
            Action<VaptchaOptions> configure)
        {
            if (null == services)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();
            services.Configure(configure);

            services.AddHttpClient(VaptchaConsts.HttpClientName)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });
                
            services.AddSingleton<IVaptchaClient, VaptchaClient>();

            return services;
        }

        public static IServiceCollection AddVaptcha(
            this IServiceCollection services, 
            IConfiguration configuration,
            string sectionName = "Vaptcha")
        {
            if (null == services)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<VaptchaOptions>(configuration.GetSection(sectionName));

            services.AddHttpClient(VaptchaConsts.HttpClientName)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() {UseProxy = false});
                
            services.AddSingleton<IVaptchaClient, VaptchaClient>();

            return services;
        }
    }
}