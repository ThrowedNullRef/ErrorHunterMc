using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Serilog;

namespace Errorhunter.Host;

public static class Program
{
    public static int Main(string[] args)
    {
        var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        var builder = WebApplication.CreateBuilder(args);
        builder.Host
               .UseSerilog(logger)
               .UseServiceProviderFactory(new LightInjectServiceProviderFactory(new ServiceContainer(ContainerOptions.Default.WithMicrosoftSettings())));

        builder.Services.AddControllers();
        builder.Services.AddServices();
        builder.Services.AddSpaStaticFiles(options => options.RootPath = KnownPaths.WebClientDir);
        
        var app = builder.Build();

        app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.MapControllers();

        app.UseSpaStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(KnownPaths.WebClientDir)
        });
        
        app.UseSpa(options =>
        {
            options.Options.SourcePath = KnownPaths.WebClientDir;
        });
        
        app.Run();
        return 1;
    }
}