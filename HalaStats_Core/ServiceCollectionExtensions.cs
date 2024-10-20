using HalaStats_Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HalaStats_Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            //services.AddHttpClient();
            services.AddScoped<IEloRatingService, EloRatingService>();
        }
    }
}