using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace senai_2s2019_CodeXP_Gufos
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
               .AddJsonOptions(options =>
               {
                   // Ignora valores nulos e interrompe o looping
                   options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               })
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //  Adiciona o Cors ao projeto
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // Adiciona o swagger
            services.AddSwaggerGen(c =>
            {
                // Define informações básicas da documentação
                c.SwaggerDoc("v1", new Info { Title = "API Gufos", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            

            // Implementa autenticação
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                // Define as opções 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Quem esta solicitando
                    ValidateIssuer = true,

                    // Quem esta validadando
                    ValidateAudience = true,

                    // Definindo o tempo de expiração
                    ValidateLifetime = true,

                    // Forma de criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ThisIsMyGufosSecretKey")),

                    // Tempo de expiração do Token
                    ClockSkew = TimeSpan.FromMinutes(30),

                    // Nome da Issuer, de onde esta vindo
                    ValidIssuer = "gufos.com",

                    // Nome da Audience, de onde esta vindo
                    ValidAudience = "gufos.com"
                };
            });
        }    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Define o uso de arquivos estáticos
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gufos");
            });

            // Define o uso de autenticação
            app.UseAuthentication();

            // Habilita o Cors
            app.UseCors("CorsPolicy");

            // Define o uso de MVC
            app.UseMvc();
        }
    }
}
