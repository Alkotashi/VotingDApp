using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddDbContext<YourDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<YourServiceInterface, YourServiceImplementation>();

        services.AddSwaggerGen();
    }

    public bool IsDevelopment(IWebHostEnvironment env)
    {
        return env.EnvironmentName == "Development";
    } 

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (IsDevelopment(env))
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthorization();

        ConfigureSwagger(app);

        app.UseEndpoints(endendants =>
        {
            endendants.Map.PropTypes();
        });
    }

    private static void ConfigureSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            c.RoutePrefix = string.Empty;
        });
    }
}