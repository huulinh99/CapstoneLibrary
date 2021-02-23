using AutoMapper;
using Capstone.Api.Controllers;
using Capstone.Core.CustomEntities;
using Capstone.Core.Hubs;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.BookDrawerInterfaces;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Core.Interfaces.ImageInterfaces;
using Capstone.Core.Services;
using Capstone.Infrastructure.Data;
using Capstone.Infrastructure.Filters;
using Capstone.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Web.Http;

namespace Capstone.Api
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
            IdentityModelEventSource.ShowPII = true;
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // options.SuppressModelStateInvalidFilter = true;
            });          
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.AddDbContext<CapstoneContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Capstone"))
            );
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IBookGroupService, BookGroupService>();
            services.AddTransient<IBookShelfService, BookShelfService>();
            services.AddTransient<IDrawerService, DrawerService>();
            services.AddTransient<IErrorMessageService, ErrorMessageService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IBorrowBookService, BorrowBookService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IBorrowDetailService, BorrowDetailService>();
            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IReturnBookService, ReturnBookService>();
            services.AddTransient<IReturnDetailService, ReturnDetailService>();
            services.AddTransient<IBookCategoryService, BookCategoryService>();
            services.AddTransient<IBookRecommendService, BookRecommendService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IBookDrawerService, BookDrawerService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IFavouriteCategoryService, FavouriteCategoryService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddScoped<MessageHub>();
            services.AddCors();

            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Capstone Library API", Version = "v1" });
            });

            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };
            });          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
            app.UseRouting();           

            

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Capstone Library API");
                options.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/message");
            });
        }
    }
}
