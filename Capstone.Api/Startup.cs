using AutoMapper;
using Capstone.Core.CustomEntities;
using Capstone.Core.Interfaces;
using Capstone.Core.Services;
using Capstone.Infrastructure.Data;
using Capstone.Infrastructure.Filters;
using Capstone.Infrastructure.Repositories;
using Capstone.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options => {
                options.SuppressModelStateInvalidFilter = true;
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
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Capstone Library API", Version = "v1" });
            });

            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    //.AllowCredentials();
                });
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
            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Capstone Library API");
                options.RoutePrefix = string.Empty;
            });



            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
