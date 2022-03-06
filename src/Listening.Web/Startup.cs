using Listening.Web.Extensions;
using Listening.Web.SignalR;
using Listening.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Collections.Generic;
using System;
using Serilog;
using Listening.Infrastructure.Repositories.Postgres;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using Listening.Infrastructure.Services;
using Microsoft.AspNetCore.StaticFiles;
//using Microsoft.AspNetCore.Builder;

namespace Listening.Web
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "AllowSpecificOrigins";

        // Order or run
        //1) Constructor
        //2) Configure services
        //3) Configure
        private IWebHostEnvironment HostingEnvironment { get; }
        public static IConfiguration Configuration { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            HostingEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services /*IHostingEnvironment env*/)
        {
            //var env = ServiceProvider.GetService<IHostingEnvironment>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();

            services.AddResponseCompression();

            services.AddCustomDbContext(Configuration);

            services.AddCustomIdentity();

            services.AddCustomOpenIddict(HostingEnvironment);

            services.AddMemoryCache();

            services.RegisterCustomServices();

            services.RegisterListeningTextServices();

            services.RegisterAdditionalServices();

            services.RegisterBlogServices();

            if (HostingEnvironment.IsDevelopment())
                services.AddSignalR(hubOptions =>
                {
                    hubOptions.EnableDetailedErrors = true;
                }).AddMessagePackProtocol();
            else
                services.AddSignalR().AddMessagePackProtocol();

            services.AddCustomLocalization(HostingEnvironment);

            services.RegisterCORS(AllowSpecificOrigins, HostingEnvironment, Configuration);

            services.AddCustomizedMvc(AllowSpecificOrigins);

            services.AddJobSchedule();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/listening";
            });

            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new Info { Title = "Listening", Version = "v1" });
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Listening", Version = "v1" });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.RegisterMapping();

            services.BuildAppId();

            services.CreateFolders(HostingEnvironment, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {

            // app.AddCustomSecurityHeaders();

            if (env.IsDevelopment())
            {
                app.AddDevMiddlewares();
            }
            else
            {
                //app.UseHsts();
                app.UseResponseCompression();
            }

            app.AddCustomLocalization();

            //app.UseHttpsRedirection();

            // https://github.com/openiddict/openiddict-core/issues/518
            // And
            // https://github.com/aspnet/Docs/issues/2384#issuecomment-297980490
            var forwarOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwarOptions.KnownNetworks.Clear();
            forwarOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwarOptions);

            app.UseAuthentication();

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".vtt"] = "text/vtt";
            provider.Mappings[".srt"] = "text/srt";
            provider.Mappings[".djvu"] = "image/vnd.djvu";
            provider.Mappings[".djv"] = "image/vnd.djvu";


            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    OnPrepareResponse = s =>
            //    {
            //        if (s.Context.Request.Path.StartsWithSegments(new PathString("/archive")) &&
            //           !s.Context.User.Identity.IsAuthenticated)
            //        {
            //            s.Context.Response.StatusCode = 401;
            //            s.Context.Response.Body = Stream.Null;
            //            s.Context.Response.ContentLength = 0;
            //        }
            //    }
            //});

            app.UseSpaStaticFiles();

            app.UseCookiePolicy();

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<Chat>("/chathub");
            //    routes.MapHub<StreamHub>("/streamHub");
            //    routes.MapHub<ShapeHub>("/shapeHub");
            //});

            //app.map


            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "set-language",
                    pattern: "setlanguage",
                    defaults: new { controller = "Home", action = "SetLanguage" });

                endpoints.MapRazorPages();

                //endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHub<Chat>("/chathub");
                endpoints.MapHub<StreamHub>("/streamHub");
                endpoints.MapHub<ShapeHub>("/shapeHub");
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //       name: "default",
            //       template: "{controller}/{action=Index}/{id?}");

            //    // http://stackoverflow.com/questions/25982095/using-googleoauth2authenticationoptions-got-a-redirect-uri-mismatch-error
            //    // routes.MapRoute(name: "signin-google", template: "signin-google", defaults: new { controller = "Account", action = "ExternalLoginCallback" });

            //    routes.MapRoute(name: "set-language", template: "setlanguage", defaults: new { controller = "Home", action = "SetLanguage" });

            //});

            // TODO: implement access restrictions
            //app.AddRestrictionsForSpec(Configuration["Data:FileStorage:Archive"]);

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                /*
                // If you want to enable server-side rendering (SSR),
                // [1] In Listening.csproj, change the <BuildServerSideRenderer> property
                //     value to 'true', so that the SSR bundle is built during publish
                // [2] Uncomment this code block
                */

                //   spa.UseSpaPrerendering(options =>
                //    {
                //        options.BootModulePath = $"{spa.Options.SourcePath}/dist-server/main.bundle.js";
                //        options.BootModuleBuilder = env.IsDevelopment() ? new AngularCliBuilder(npmScript: "build:ssr") : null;
                //        options.ExcludeUrls = new[] { "/sockjs-node" };
                //        options.SupplyData = (requestContext, obj) =>
                //        {
                //          //  var result = appService.GetApplicationData(requestContext).GetAwaiter().GetResult();
                //          obj.Add("Cookies", requestContext.Request.Cookies);
                //        };
                //    });

                if (env.IsDevelopment())
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                //else
                //    spa.UseAngularCliServer(npmScript: "start");

                ServiceProvider = app.ApplicationServices;
            });

            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            new ChatCleanRepository().CleanupSignalRIds().GetAwaiter().GetResult();
            DebianCleanupService.Cleanup();
        }
    }
}
