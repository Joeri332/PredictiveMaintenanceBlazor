using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Database;
using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Services;
using Microsoft.AspNetCore.StaticFiles; // Add this for FileExtensionContentTypeProvider

namespace PredictiveMaintenance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPeopleService, PeopleService>();
            builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
            //By adding addhttpClient whenever this service is used, it makes sure it has an client in its constructor.
            builder.Services.AddHttpClient<IPredictionService, PredictionService>();
            builder.Services.AddScoped<IOperatorFeedbackService, OperatorFeedbackService>();
            builder.Services.AddScoped<IScoreService, ScoreService>();
            builder.Services.AddSingleton<ModelSelectionService>();
            // Add a scope to ensure the database is created
            var scopeFactory = builder.Services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated(); // This will create the database and tables if they don't exist
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Configure the ContentTypeProvider for .dae files
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".dae"] = "text/xml";
            //provider.Mappings[".obj"] = "text/plain";


            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
