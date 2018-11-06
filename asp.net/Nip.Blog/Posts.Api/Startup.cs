using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Posts.Api.Data;
using Posts.Api.Models;
using Posts.Api.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Posts.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
        public ILogger<Startup> _logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvcCore().AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddApiVersioning(options => options.ReportApiVersions = true);

            _logger.LogInformation("Adding Swagger documentation generator");
            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new Info{Title = "Blog Posts API", Version = description.ApiVersion.ToString()});
                    }
                });


            //services.AddDbContext<BlogPostContext>(opt => opt.UseInMemoryDatabase("BlogPosts"));

            //var connection = @"Server=(localdb)\mssqllocaldb;Database=BlogPostsDb;Trusted_Connection=True;ConnectRetryCount=0";
            //services.AddDbContextPool<BlogPostContext>(options => options.UseSqlServer(connection));

            var connection = @"Data Source=Data/Posts.db";
            services.AddDbContextPool<BlogPostContext>(opt => opt.UseSqlite(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BlogPostContext context, IApiVersionDescriptionProvider apiVersionDescProvider)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                context.Database.EnsureCreated();
                if (!context.BlogPosts.Any())
                {
                    var posts = new List<BlogPost>();
                    for (int i = 1; i < 6; i++)
                    {
                        posts.Add(createBlogPost("title" + i, "desc" + i));
                    }
                    context.BlogPosts.AddRange(posts);
                    context.SaveChanges();
                }
            }
            else
            {
                app.UseExceptionHandler("/api/v1/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            _logger.LogInformation("Adding Swagger UI");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in apiVersionDescProvider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                c.RoutePrefix = string.Empty; // serve the Swagger UI at the app's root
            });


        }


        private BlogPost createBlogPost(String title, String desc)
        {
            BlogPost blogPost = new BlogPost();
            blogPost.Title = title;
            blogPost.Description = desc;
            return blogPost;
        }
    }
}
