using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elephantshine.Models;
using elephantshine.Models.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Parser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NetEscapades.AspNetCore.SecurityHeaders;

namespace elephantshine
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            CurrentEnvironment = env;
        }

        public IHostingEnvironment CurrentEnvironment { get; set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddCustomHeaders();

            if (CurrentEnvironment.IsProduction())
            {
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                });
            }

            services.Configure<List<Portfolio>>(Configuration.GetSection("Portfolios"));
            services.Configure<List<EnumCategory>>(Configuration.GetSection("EnableCategorys"));
            services.Configure<Settings>(Configuration.GetSection("Settings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var policyCollection = new HeaderPolicyCollection()
                .AddReferrerPolicyNoReferrer()
                .AddFrameOptionsSameOrigin()
                .AddXssProtectionBlock()
                .AddContentTypeOptionsNoSniff()
                .AddCustomHeader("Content-Security-Policy",
                    $"default-src 'none'; script-src 'self' 'unsafe-inline' 'unsafe-eval' az416426.vo.msecnd.net use.typekit.net www.googletagmanager.com; connect-src 'self' p.typekit.net use.typekit.net dc.services.visualstudio.com; img-src 'self' 'unsafe-inline' data: ; style-src 'self' 'unsafe-inline'; font-src 'self' use.typekit.net")
                .AddCustomHeader("Strict-Transport-Security", "max-age=31536000; includeSubDomains");

            app.UseCustomHeadersMiddleware(policyCollection);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=31536000";
                    ctx.Context.Response.Headers[HeaderNames.Expires] = DateTime.UtcNow.AddYears(1).ToString("R");
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

        }
    }
}
