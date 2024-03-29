using HousingProject.Architecture.Constants;
using HousingProject.Architecture.CRUDServices.Email;
using HousingProject.Architecture.Data;
using HousingProject.Architecture.HouseRegistration_Services;
using HousingProject.Architecture.IHouseRegistration_Services;
using HousingProject.Architecture.Interfaces.IEmail;
using HousingProject.Architecture.Interfaces.ILandlordModel;
using HousingProject.Architecture.Interfaces.IlogginServices;
using HousingProject.Architecture.Interfaces.IRenteeServices;
using HousingProject.Architecture.IPeopleManagementServvices;
using HousingProject.Architecture.PeopleManagementServices;
using HousingProject.Architecture.Services.Landlord;
using HousingProject.Architecture.Services.Rentee.Services;
using HousingProject.Architecture.Services.User_Login;
using HousingProject.Core.Models.Email;
using HousingProject.Core.Models.People;
using HousingProject.Infrastructure.CRUDServices.HouseRegistration_Services.HouseUnitsServices;
using HousingProject.Infrastructure.CRUDServices.MainPaymentServices;
using HousingProject.Infrastructure.CRUDServices.N_IMages_Services;
using HousingProject.Infrastructure.CRUDServices.Payments.Daraja;
using HousingProject.Infrastructure.CRUDServices.Payments.Rent;
using HousingProject.Infrastructure.CRUDServices.ProfessionalsServices;
using HousingProject.Infrastructure.CRUDServices.UsersExtra;
using HousingProject.Infrastructure.ExtraFunctions;
using HousingProject.Infrastructure.ExtraFunctions.Checkroles.ChcekRoles;
using HousingProject.Infrastructure.ExtraFunctions.Checkroles.IcheckRole;
using HousingProject.Infrastructure.ExtraFunctions.GenerateWorkId;
using HousingProject.Infrastructure.ExtraFunctions.IExtraFunctions;
using HousingProject.Infrastructure.ExtraFunctions.Images;
using HousingProject.Infrastructure.ExtraFunctions.LoggedInUser;
using HousingProject.Infrastructure.ExtraFunctions.RolesDescription;
using HousingProject.Infrastructure.Interfaces.IDarraja;
using HousingProject.Infrastructure.Interfaces.IHouseRegistration_Services;
using HousingProject.Infrastructure.Interfaces.IProfessionalsServices;
using HousingProject.Infrastructure.Interfaces.ITenantStatementServices;
using HousingProject.Infrastructure.Interfaces.IUserExtraServices;
using HousingProject.Infrastructure.JobServices;
using HousingProject.Infrastructure.JobServices.Payment_Receipts;
using HousingProject.Infrastructure.JobServices.tenantjobs;
using HousingProject.Infrastructure.SuperServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Security.Claims;
using System.Text;

namespace HousingProject.API
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


            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddControllers().AddNewtonsoftJson();
            services.AddIdentity<RegistrationModel, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<HousingProjectContext>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobkey = new JobKey("Emailjob");
                q.AddJob<Emailjob>(z => z.WithIdentity(jobkey));
                q.AddTrigger(y => y.ForJob(jobkey)
                .WithIdentity("Emailjob-trigger")
                .WithCronSchedule("0/58 * * * * ?"));

                //automated rent payday
                var automatedrentpaymentkey = new JobKey("automatedMail");
                q.AddJob<automatedMail>(z => z.WithIdentity(automatedrentpaymentkey));
                q.AddTrigger(y => y.ForJob(automatedrentpaymentkey)
                .WithIdentity("automatedMail-trigger")
                .WithCronSchedule("0 0 12 5 1/1 ? *"));
                //.WithCronSchedule("0 0/5 * 1/1 * ? *"));


                //automated rent payday
                var monthly_rent_Update_key = new JobKey("Monthly_Rent_Update");
                q.AddJob<Monthly_Rent_Update>(z => z.WithIdentity(monthly_rent_Update_key));
                q.AddTrigger(y => y.ForJob(monthly_rent_Update_key)
                .WithIdentity("Monthly_Rent_Update-trigger")
                .WithCronSchedule("0 0 0 1 * ? *"));
                // .WithCronSchedule("0/1 * * * * ?"));

                //automated payment  receipt sender
                var send_payment_receipts_key = new JobKey("Payment_Receipt_Job");
                q.AddJob<Payment_Receipt_Job>(z => z.WithIdentity(send_payment_receipts_key));
                q.AddTrigger(y => y.ForJob(send_payment_receipts_key)
                .WithIdentity("Payment_Receipt_Job-trigger")
                //.WithCronSchedule("0 0 0 1 * ? *"));
                 .WithCronSchedule("0 0/1 * 1/1 * ? *"));

                //automated rent payday
                var Back_monthly_update_key = new JobKey("Reset_Updated_this_month");
                q.AddJob<Back_monthly_update>(z => z.WithIdentity(Back_monthly_update_key));
                q.AddTrigger(y => y.ForJob(Back_monthly_update_key)
                .WithIdentity("Back_monthly_update-trigger")
                 // .WithCronSchedule("0/2 * * * * ?"));
                 .WithCronSchedule("0 0/1 * 1/1 * ? *"));
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            services.AddHttpClient("mpesa", m =>
            {
                m.BaseAddress =
               new System.Uri("https://sandbox.safaricom.co.ke"

);
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });


            services.AddDbContext<HousingProjectContext>(
                x => x.UseSqlServer(Configuration.GetConnectionString("DevConnectiions"))
            );
            //services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "HousingProject.API", Version = "v1" }
                );
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please Insert token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "Jwt",
                        Scheme = "bearer"
                    }
                );
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
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
                    }
                );
                ;
            });
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.Configure<IdentityOptions>(options =>

                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier
            );

            services.AddHttpContextAccessor();
            services.AddScoped<IRegistrationServices, RegistrationServices>();
            services.AddScoped<IHouse_RegistrationServices, House_RegistrationServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IverificationGenerator, verificationtokenGenerator>();
            services.AddScoped<ILandlordServices, LanlordServices>();
            services.AddScoped<ITenantServices, TenantServices>();
            services.AddScoped<IpaymentServices, PaymentServices>();
            services.AddScoped<IloggedInServices, UserLoginServices>();
            services.AddScoped<IextraFunctions, AddingCountiesCRUD>();
            services.AddScoped<IImagesServices, ImagesServices>();
            services.AddScoped<IRoles, Roles>();
            services.AddScoped<ILoggedIn, LoggedIn>();
            services.AddScoped<ITenantStatementServices, TenantStatementServices>();
            services.AddScoped<IHouseUnits, HouseUnitsServices>();
            services.AddScoped<IProfessionalsServices, ProfessionalServices>();
            services.AddScoped<IGenerateIdService, GenerateIdService>();
            services.AddScoped<ICheckroles, CheckRoles>();
            services.AddScoped<IAdminServices, AdminService>();
            
            services.AddScoped<IUserExtraServices, UserExtraServices>();
         
            services.AddCors();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddScoped<In_ImagesServices, n_images_services>();
            services.AddScoped<IDarajaServices, Daraja_Services>();
            services.AddScoped<IpaymentServices, PaymentServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HousingProject.API v1")
                );
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin() 
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
