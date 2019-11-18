namespace TodoApi.Extensions
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Serialization;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using TodoApi.MappingProfiles;
    using TodoApi.Models;
    using TodoApi.Models.Validations;
    using TodoApi.Services.Todo;

    public static class Extensions
    {
        private static readonly string _nameCorsPolicy = "allowOrigins";

        /// <summary>
        /// add CORS to service container
        /// </summary>
        /// <param name="services">the service container</param>
        public static void AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    _nameCorsPolicy,
                    builders =>
                    {
                        builders.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
        }

        public static void RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {
            // setting of connection to database 
            services.Configure<TodoDatabaseSettings>(Configuration.GetSection(nameof(TodoDatabaseSettings)));

            // ---
            services.AddSingleton<ITodoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TodoDatabaseSettings>>().Value);

            // inject auto mapper configuration
            services.AddAutoMapper(typeof(TodoProfile).Assembly);

            // register services
            services.AddScoped<ITodoService, TodoService>();
        }

        /// <summary>
        /// configure Swagger for API Documentation
        /// </summary>
        /// <param name="services">the DI service Collection</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Configure the XML comments file path for the Swagger JSON and UI.
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile));

                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();

                c.SwaggerDoc("Version 1", new Info
                {
                    Version = "Version 1",
                    Title = "TODO API",
                    Description = "Web Api TODO",
                    TermsOfService = "None"
                });
            });
        }

        /// <summary>
        /// configuration fluent validation
        /// </summary>
        /// <param name="mvcBuilder"></param>
        public static IMvcBuilder ConfigFluentValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<TodoModelValidation>();
            });
            return mvcBuilder;
        }

        /// <summary>
        /// configuration of JSON response
        /// </summary>
        /// <param name="mvcBuilder"></param>
        public static IMvcBuilder ConfigureJsonResponse(this IMvcBuilder mvcBuilder) {
            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            return mvcBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseCorsPipeline(this IApplicationBuilder app)
        {
            app.UseCors(_nameCorsPolicy);
        }

        public static void UseSwaggerPipeline(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });
        }
    }
}
