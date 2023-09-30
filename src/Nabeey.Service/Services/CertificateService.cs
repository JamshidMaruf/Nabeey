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

namespace Nabeey.Service.Services;

public class CertificateService : ICertificateService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userService;
    private readonly IRepository<Quiz> quizService;
    private readonly IRepository<Asset> assetRepository;
    private readonly IRepository<Certificate> repository;
    public CertificateService(IMapper mapper,
        IRepository<Quiz> quizService,
        IRepository<User> userService,
        IRepository<Asset> assetRepository,
        IRepository<Certificate> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
        this.quizService = quizService;
        this.assetRepository = assetRepository;
    }

    public async ValueTask<CertificateResultDtoDto> GenerateAsync(CertificateCreationDto dto)
    {
        var user = await userService.SelectAsync(e => e.Id == dto.UserId)
            ?? throw new NotFoundException("This user is not found");

        var quiz = await quizService.SelectAsync(e => e.Id == dto.QuizId)
            ?? throw new NotFoundException("This quiz is not found");

        var fullName = PadBoth($"{user.FirstName} {user.LastName}", 50);
        var quizName = PadBoth(quiz.Name, 80);
        var score = dto.Score.ToString();
        var temp = "Templates/certificate_template.jpg";

        string filePath = Path.Combine(PathHelper.WebRootPath, temp);
        using (var bitmap = new Bitmap(filePath))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                Brush brush = new SolidBrush(Color.FromArgb(44, 137, 132));

                var id = Guid.NewGuid().ToString("N");

                var defaultFont = new Font("sail", 50, FontStyle.Regular);
                var fullNameFont = new Font("sail", 50, FontStyle.Bold);

                var sizeOfCertNumber = graphics.MeasureString(id, defaultFont);
                var sizeOfFullName = graphics.MeasureString(fullName, fullNameFont);
                var sizeOfSubject = graphics.MeasureString(quizName, defaultFont);
                var sizeOfTotalScore = graphics.MeasureString(score, defaultFont);

                graphics.DrawString(id, defaultFont, brush, new PointF((bitmap.Width - sizeOfCertNumber.Width) / 2.7f, 1300));
                graphics.DrawString(fullName, fullNameFont, brush, new PointF((bitmap.Width - sizeOfFullName.Width), 100));
                graphics.DrawString(quizName, defaultFont, brush, new PointF((bitmap.Width - sizeOfSubject.Width), 220));
                graphics.DrawString(score, defaultFont, brush, new PointF((bitmap.Width - sizeOfTotalScore.Width) / 1.65f, 500));

                string outputFilePath = Path.Combine(PathHelper.WebRootPath, "Certificates/" + id + ".png");
                using (FileStream fileStream = File.Create(outputFilePath))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }

                Asset asset = new()
                {
                    FileName = id + ".png",
                    FilePath = "Certificates/" + id + ".png"
                };
                await assetRepository.InsertAsync(asset);

                Certificate entity = new()
                {
                    FileId = asset.Id,
                    QuizId = quiz.Id,
                    UserId = user.Id,
                };
                await repository.InsertAsync(entity);
                await repository.SaveAsync();

                return mapper.Map<CertificateResultDtoDto>(entity);
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

    public async ValueTask<IEnumerable<CertificateResultDtoDto>> RetriveUserCertificatesAsync(long userId)
    {
        var entities = await repository.SelectAll(e => e.UserId == userId, new[] { "File" }).ToListAsync();
        return mapper.Map<IEnumerable<CertificateResultDtoDto>>(entities);
    }

    public async ValueTask<IEnumerable<CertificateResultDtoDto>> RetrieveByQuizIdCertificateAsync(long userId, long quizId)
    {
        var certificate = await repository.SelectAll(c => c.UserId == userId && c.QuizId == quizId).ToListAsync();
        return mapper.Map<IEnumerable<CertificateResultDtoDto>>(certificate);
    }

    public async ValueTask<CertificateResultDtoDto> RetrieveByIdAsync(long id)
    {
        var entity = await repository.SelectAsync(a => a.Id == id, new[] { "File" })
            ?? throw new DirectoryNotFoundException($"This Certificate is not found by ID = {id}");

        return mapper.Map<CertificateResultDtoDto>(entity);
    }

    public async ValueTask<IEnumerable<CertificateResultDtoDto>> RetrieveAllAsync()
    {
        var entities = await repository.SelectAll(includes: new[] { "File" }).ToListAsync();
        return mapper.Map<IEnumerable<CertificateResultDtoDto>>(entities);
    }

}