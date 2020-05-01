using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Data;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            //services.AddSingleton<IResturantData, InMemoryResturantData>();
            services.AddScoped<IResturantData, SqlResturantData>(); 

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //middlewares pipeline is bi-directional. 
            //Any middleware kept in the begining of this method ensures either it executes when request comes in or goes out.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    //UseDeveloperExceptionPage(mostly like stack trace) middleware is installed in the application pipeline. this make sure to check response when it goes out.
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseHsts(); //instructs the browser to access information over secured connection.
            }

            //Custom middleware; to server other middleware, move it after UseMvc() middleware 
            //in the pipeline or configre the middleware to server other middleware in pipeline
            app.Use(SayHelloMiddleware);

            app.UseStaticFiles();
            app.UseNodeModules(env);   // to use static files from node modules folder
            app.UseCookiePolicy();
            //app.UseAuthentication();
            //app.UseSignalR();   //real time socket connection
            app.UseMvc();

            //app.Use(SayHelloMiddleware);

        }

        //private RequestDelegate SayHelloMiddleware(RequestDelegate arg)
        //{
        //    //delegate; ctx is HttpContext object
        //    return async ctx =>
        //    {
        //        await ctx.Response.WriteAsync("Hello World!");
        //    };
        //}

        private RequestDelegate SayHelloMiddleware(RequestDelegate arg)
        {
            //delegate; ctx is HttpContext object
            return async ctx =>
             {
                 if (ctx.Request.Path.StartsWithSegments("/hello"))
                 {
                     await ctx.Response.WriteAsync("Hello World!");
                 }
                 else
                 {
                     await arg(ctx);    //awaiting next middleware in the pipeline(allowed other middleware to do processing)
                 }
             };
        }

    }
}
