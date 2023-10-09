using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Nabeey.DataAccess.IRepositories;
using Nabeey.DataAccess.Repositories;
using Nabeey.Service.Interfaces;
using Nabeey.Service.Mappers;
using Nabeey.Service.Services;
using System.Text;

namespace Nabeey.Web.Extensions;

public static class ServicesCollection
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddAutoMapper(typeof(MappingProfile));
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IQuizService, QuizService>();
		services.AddScoped<IBookService, BookService>();
		services.AddScoped<IAssetService, AssetService>();
		services.AddScoped<IAnswerService, AnswerService>();
		services.AddScoped<IArticleService, ArticleService>();
		services.AddScoped<IQuestionService, QuestionService>();
		services.AddScoped<IQuizResultService, QuizResultService>();
		services.AddScoped<ICertificateService, CertificateService>();
		services.AddScoped<IContentVideoService, ContentVideoService>();
		services.AddScoped<IContentAudioService, ContentAudioService>();
		services.AddScoped<IQuizQuestionService, QuizQuestionService>();
		services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
		services.AddScoped<IContentCategoryService, ContentCategoryService>();
        services.AddHttpContextAccessor();
	}
}