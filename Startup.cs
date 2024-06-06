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
        
        // Configuring the DbContext with SQL Server
        services.AddDbContext<YourDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Example Service (You should replace with actual service implementations)
        services.AddScoped<YourServiceInterface, YourServiceImplementation>();

        // Swagger Service for API Documentation
        services.AddSwaggerGen();

        // Enabling CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });
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

        // Applying the CORS policy globally (or you can apply per controller or action)
        app.UseCors("AllowAll");

        app.UseAuthorization();

        ConfigureSwagger(app);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Corrected to map controllers properly
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