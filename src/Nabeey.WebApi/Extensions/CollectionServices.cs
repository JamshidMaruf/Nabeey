using Nabeey.DataAccess.IRepositories;
using Nabeey.DataAccess.Repositories;
using Nabeey.Service.Interfaces;
using Nabeey.Service.Mappers;
using Nabeey.Service.Services;

namespace Nabeey.WebApi.Extensions;

public static class CollectionServices
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddAutoMapper(typeof(MappedProfile));
        services.AddScoped<IQuizService, QuizService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IQuizQuestionService, QuizQuestionService>();
    }
}
