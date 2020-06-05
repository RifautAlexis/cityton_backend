using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using AutoMapper;
using Cityton.Api.Contracts.Requests;
using Cityton.Api.Handlers;
using Cityton.Api.Data;
using Microsoft.AspNetCore.Http;
using Cityton.Api.Hubs;
using Cityton.Api.Contracts.DTOs;

namespace Cityton.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<
                IHandler<LoginRequest, ObjectResult>,
                LoginHandler>();
            services.AddScoped<
                IHandler<SignupRequest, ObjectResult>,
                SignupHandler>();
            services.AddScoped<
                IHandler<GetConnectedUserRequest, ObjectResult>,
                GetConnectedUserHandler>();
            services.AddScoped<
                IHandler<ChangePasswordRequest, ObjectResult>,
                ChangePasswordHandler>();
            services.AddScoped<
                IHandler<GetProfileRequest, ObjectResult>,
                GetProfileHandler>();
            services.AddScoped<
                IHandler<SearchChallengeRequest, ObjectResult>,
                SearchChallengeHandler>();
            services.AddScoped<
                IHandler<CreateChallengeRequest, ObjectResult>,
                CreateChallengeHandler>();
            services.AddScoped<
                IHandler<UpdateChallengeRequest, ObjectResult>,
                UpdateChallengeHandler>();
            services.AddScoped<
                IHandler<DeleteChallengeRequest, ObjectResult>,
                DeleteChallengeHandler>();
            services.AddScoped<
                IHandler<SearchUserRequest, ObjectResult>,
                SearchUserHandler>();
            services.AddScoped<
                IHandler<DeleteUserRequest, ObjectResult>,
                DeleteUserHandler>();
            services.AddScoped<
                IHandler<GetThreadsByUserIdRequest, ObjectResult>,
                GetThreadsByUserIdHandler>();
            services.AddScoped<
                IHandler<GetMessagesByThreadIdRequest, ObjectResult>,
                GetMessagesByThreadIdHandler>();
            services.AddScoped<
                IHandlerHub<CreateMessageDTO, ObjectResult>,
                ReceiveMessageHandler>();
            services.AddScoped<
                IHandler<int, ObjectResult>,
                RemoveMessageHandler>();
            services.AddScoped<
                IHandler<GetProgressionRequest, ObjectResult>,
                GetProgressionHandler>();
            services.AddScoped<
                IHandler<ValidateChallengeRequest, ObjectResult>,
                ValidateChallengeHandler>();
            services.AddScoped<
                IHandler<RejectChallengeRequest, ObjectResult>,
                RejectChallengeHandler>();
            services.AddScoped<
                IHandler<UndoChallengeRequest, ObjectResult>,
                UndoChallengeHandler>();
            services.AddScoped<
                IHandler<CreateGroupRequest, ObjectResult>,
                CreateGroupHandler>();
            services.AddScoped<
                IHandler<DeleteGroupRequest, ObjectResult>,
                DeleteGroupHandler>();
            services.AddScoped<
                IHandler<GetGroupInfoRequest, ObjectResult>,
                GetGroupInfoHandler>();
            services.AddScoped<
                IHandler<SearchGroupRequest, ObjectResult>,
                SearchGroupHandler>();
            services.AddScoped<IHandler<DeleteGroupRequestRequest, ObjectResult>,
                DeleteGroupRequestHandler>();
            services.AddScoped<
                IHandler<DeleteGroupMembershipRequest, ObjectResult>,
                DeleteGroupMembershipHandler>();
            services.AddScoped<IHandler<AcceptGroupRequestRequest, ObjectResult>,
                AcceptGroupRequestHandler>();
            services.AddScoped<IHandler<EditGroupNameRequest, ObjectResult>,
                EditGroupNameHandler>();
            services.AddScoped<IHandler<CreateGroupRequestRequest, ObjectResult>,
                CreateGroupRequestHandler>();
            services.AddScoped<IHandler<ChangeProfilePictureRequest, ObjectResult>,
                ChangeProfilePictureHandler>();

            services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));

            // configure strongly typed settings objects
            var secret = Configuration.GetSection("Settings:Secret").Value;

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs/chat")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //services.AddMemoryCache();

            services.AddSignalR();

            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressInferBindingSourcesForParameters = true)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseWebSockets();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hub/chatHub");
            });
        }
    }
}
