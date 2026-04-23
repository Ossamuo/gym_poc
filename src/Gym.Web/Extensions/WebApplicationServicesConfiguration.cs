using Gym.Web.Extensions.Services.Account;
using Gym.Web.Extensions.Services.Activity;
using Gym.Web.Extensions.Services.Login;
using Gym.Web.Shared;
using Gym.Web.Shared.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gym.Web.Extensions
{
    public static class WebApplicationServicesConfiguration
    {
        public static void AddWebApplicationServicesConfiguration(this WebApplicationBuilder builder)
        {


            builder.AddCreateHandler();
            builder.AddDetailHandler();
            builder.AddEditHandler();
            builder.AddLoginHandler();
            builder.AddListAcitivityHandler();
            builder.AddBokkingSessionHandler();
        }

        public static void AddCookie(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<CookieHandler>();
        }
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.ImageUrl = builder.Configuration.GetValue<string>("ImageUrl") ?? string.Empty;
      
        }
        public static void AddHttpClientConfiguration(this WebApplicationBuilder builder)
        {
            //Microsoft.Extensions.Http is a extension for HttpClient(simpler version of it)
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Environment.WebRootPath)
            });
            //Microsoft.Extensions.Http  => creates an httpclient and add some features like  add the cookies in the head for all requisitions
            builder.Services.AddHttpClient(Configuration.HttpClientName, opt =>
            {
                opt.BaseAddress = new Uri(Configuration.BackendUrl);
            })
            .AddHttpMessageHandler<CookieHandler>(); //=>Intercept all the messages and attached the authentication and inspect the cookies
            ;
        }

        public static void AddAuthorizationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication((options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })) 
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login"; 
                    options.AccessDeniedPath = "/AccessDenied";
                    options.Cookie.Name = "GymApp_Auth"; 
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });
        }
    }
}
