using AutoMapper;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Nabeey.Service.Helpers;
using Nabeey.Service.Interfaces;
using Nabeey.Service.Exceptions;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Entities.Assets;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Service.DTOs.Certificates;
using Nabeey.Domain.Entities.Certificates;
using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.Services;

public class CertificateService : ICertificateService
{
    private readonly IMapper mapper;
    private readonly HttpRequest httpsRequest;
    private readonly IRepository<User> userService;
    private readonly IRepository<Quiz> quizService;
    private readonly IRepository<Asset> assetRepository;
    private readonly IRepository<Certificate> repository;
    public CertificateService(IMapper mapper,
        IRepository<Quiz> quizService,
        IRepository<User> userService,
        IRepository<Asset> assetRepository,
        IRepository<Certificate> repository,
        IHttpContextAccessor httpsContextAccessor)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
        this.quizService = quizService;
        this.assetRepository = assetRepository;
        this.httpsRequest = httpsContextAccessor.HttpContext.Request;
    }

    public async ValueTask<CertificateResultDto> GenerateAsync(CertificateCreationDto dto)
    {
        var user = await userService.SelectAsync(e => e.Id == dto.UserId)
            ?? throw new NotFoundException("This user is not found");

        var quiz = await quizService.SelectAsync(e => e.Id == dto.QuizId)
            ?? throw new NotFoundException("This quiz is not found");

        var fullName = PadBoth($"{user.FirstName} {user.LastName}", 50);
        var score = PadBoth(Math.Round(dto.Score, 1).ToString(), 5);
        var quizName = PadBoth(quiz.Name, 80);
        var date = DateTime.Now.ToString().Split().First();
        var temp = "Templates/certificate_template.jpg";

        string filePath = Path.Combine(PathHelper.WebRootPath, temp);
        using (var bitmap = new Bitmap(filePath))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                Brush brush = new SolidBrush(Color.FromArgb(168, 126, 93));

                var id = Guid.NewGuid().ToString("N");

                var fullNameFont = new Font("ebrima", 50, FontStyle.Bold);
                var defaultFont = new Font("Gabarito", 40, FontStyle.Regular);

                var sizeOfFullName = graphics.MeasureString(fullName, fullNameFont);
                var sizeOfTotalScore = graphics.MeasureString(score, defaultFont);
                var sizeofDate = graphics.MeasureString(date, defaultFont);

                graphics.DrawString(fullName, fullNameFont, brush, new PointF((bitmap.Width - sizeOfFullName.Width)/2.1f, 1505));
                graphics.DrawString(score, defaultFont, brush, new PointF((bitmap.Width - sizeOfTotalScore.Width) / 1.52f, 2350));
                graphics.DrawString(date, defaultFont, brush, new PointF((bitmap.Width - sizeofDate.Width) / 1.46f, 3510));



                string outputFilePath = Path.Combine(PathHelper.WebRootPath, "Certificates/" + id + ".png");
                using (FileStream fileStream = File.Create(outputFilePath))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }

                Asset asset = new()
                {
                    FileName = id + ".png",
                    FilePath = $"{httpsRequest.Scheme}://{httpsRequest.Host}/Certificates/{id}.png"
                };

                var a = httpsRequest.Scheme;
                var s = httpsRequest.Host;

                var p = asset.FilePath;


                await assetRepository.InsertAsync(asset);
                await assetRepository.SaveAsync();

                Certificate entity = new()
                {
                    FileId = asset.Id,
                    QuizId = quiz.Id,
                    UserId = user.Id
                };
                await repository.InsertAsync(entity);
                await repository.SaveAsync();

                return mapper.Map<CertificateResultDto>(entity);
            }
        }
    }

    private static string PadBoth(string source, int length)
    {
        int spaces = length - source.Length;
        int padLeft = spaces / 2 + source.Length;
        return source.PadLeft(padLeft).PadRight(length);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var asset = await assetRepository.SelectAsync(a => a.Id == id)
            ?? throw new DirectoryNotFoundException($"This Certificate is not found by ID = {id}");

        assetRepository.Delete(asset);
        File.Delete(asset.FilePath);
        return true;
    }

    public async ValueTask<IEnumerable<CertificateResultDto>> RetriveUserCertificatesAsync(long userId)
    {
        var entities = await repository.SelectAll(e => e.UserId == userId, new[] { "File" }).ToListAsync();
        return mapper.Map<IEnumerable<CertificateResultDto>>(entities);
    }

    public async ValueTask<IEnumerable<CertificateResultDto>> RetrieveByQuizIdCertificateAsync(long userId, long quizId)
    {
        var certificate = await repository.SelectAll(c => c.UserId == userId && c.QuizId == quizId, new[] { "File" }).ToListAsync();
        return mapper.Map<IEnumerable<CertificateResultDto>>(certificate);
    }

    public async ValueTask<CertificateResultDto> RetrieveByIdAsync(long id)
    {
        var entity = await repository.SelectAsync(a => a.Id == id, new[] { "File" })
            ?? throw new DirectoryNotFoundException($"This Certificate is not found by ID = {id}");

        return mapper.Map<CertificateResultDto>(entity);
    }

    public async ValueTask<IEnumerable<CertificateResultDto>> RetrieveAllAsync()
    {
        var entities = await repository.SelectAll(includes: new[] { "File" }).ToListAsync();
        return mapper.Map<IEnumerable<CertificateResultDto>>(entities);
    }

}