using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OnlineShop.Persistence;
using MediatR;
using OnlineShop.Application.Interfaces;
using OnlineShop.WebApi.Filters;
using AutoMapper;
using OnlineShop.Application.Users.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using OnlineShop.Application.Helpers;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Utils.Tokens;
using OnlineShop.Persistence.Data;
using OnlineShop.Persistence.Interfaces;
using OnlineShop.WebApi.Services;
using OnlineShop.Application.AutoMapper;
using OnlineShop.Application.Products.Queries;
using OnlineShop.Persistence.Concrete;
using OnlineShop.Application.Services;
using System.IO;
using OnlineShop.WebApi.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace OnlineShop.WebApi
{
    public class Startup
    {
        public bool IsDevelopment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            IsDevelopment = env.IsDevelopment();
        }

        public IConfiguration Configuration { get; }

        private string FilesPhysicalRootPath => Configuration.GetSection("FileStorage")["PhysicalRootPath"];

        private string FilesVirtualPath => Configuration.GetSection("FileStorage")["VirtualPath"];

        private string FilesRelativePath => FilesVirtualPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IOnlineShopDbContext, OnlineShopDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(new Assembly[] { typeof(ProductProfile).GetTypeInfo().Assembly });

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetProductDetailsQueryHandler).GetTypeInfo().Assembly);
            services.AddScoped(typeof(ICurrentUser), typeof(HttpContextCurrentUser));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();
            services.AddScoped<ISeeder<OnlineShopDbContext>, OnlineShopSeeder>();
            services.AddScoped<IImageProcessor, ImageProcessor>();

            //services.ConfigureOptions<ImageThumbnailConfiguration>();
            services.AddScoped<IImageFileService, ImageFileService>();

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options => options.AddPolicy("AllowAll", p => p
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "LoadProPlus", Version = "v1" });
            });

            if (IsDevelopment)
            {
                services.AddScoped<IFileService>(o => new FileService(FilesPhysicalRootPath, FilesRelativePath, FilesVirtualPath));
            }

            services.AddConfigOptions<ImageThumbnailConfiguration>(Configuration, "ImageThumbnailConfiguration");

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // Add Bearer Authentication
            services.AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(authentication =>
            {
                authentication.RequireHttpsMetadata = false;
                authentication.SaveToken = true;
                authentication.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                CreateRootPath();

                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(FilesPhysicalRootPath, FilesRelativePath)),
                    RequestPath = new PathString(FilesVirtualPath)
                });

                TelemetryConfiguration.Active.DisableTelemetry = true;
                TelemetryDebugWriter.IsTracingDisabled = true;
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
        private void CreateRootPath()
        {
            if (!Directory.Exists(FilesPhysicalRootPath))
            {
                Directory.CreateDirectory(FilesPhysicalRootPath);
            }
        }

    }
}
