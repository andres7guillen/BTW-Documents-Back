using BTW.Application.Services.DocumentHistory;
using BTW.Application.Services.DocumentLogService;
using BTW.Application.Services.DocumentService;
using BTW.Domain.Repositories;
using BTW.Infrastructure.Repositories;

namespace BTW.API.Utilities;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDocumentHistoryRepository, DocumentHistoryRepository>();
        services.AddScoped<IDocumentLogRepository, DocumentLogRepository>();

        //Services
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IDocumentHistoryService, DocumentHistoryService>();
        services.AddScoped<IDocumentLogService, DocumentLogService>();

        return services;
    }
}
