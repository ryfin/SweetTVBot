using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using SweetTVChallengeBot.Handlers;
using SweetTVChallengeBot.Storage;
using SweetTVChallengeBot.Models;
using System.Net;
using SweetTVChallengeBot.Security;

namespace SweetTVChallengeBot
{
    public class Startup
    {
        public static ITelegramBotClient botClient;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));

            services.AddTransient<IHandlerResolver, HandlerResolver>();
            services.AddTransient<IStorageProvider<Movie>, StorageProvider>();
            services.AddTransient<IStorageProvider<ICredentials>, CredentialsStorage>();
            services.AddTransient<ITelegramClientFactory, TelegramClientFactory>();

            services.AddSingleton<ISecretsProvider, SecretsProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
