using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Persistance.Repository;
using YTubers.Web.Services;

namespace YTubers
{
    public static class PersistanceLayer
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IYuTuberRepository, YuTuberRepository>();
            services.AddTransient<IReachRepository, ReachRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddScoped<IYouTubeFacade, YouTubeFacade>();
        }
    }
}
