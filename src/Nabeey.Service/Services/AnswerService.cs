using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;


public class AnswerService : IAnswerService
{
	private readonly IMapper mapper;
	private readonly IRepository<Answer> repository;
	private readonly IAssetService assetService;
	private readonly IRepository<Question> quetionRepository;

	public AnswerService(IRepository<Answer> repository, IMapper mapper, IRepository<Question> quetionRepository, IAssetService assetService)
	{
		this.mapper = mapper;
		this.repository = repository;
		this.assetService = assetService;
		this.quetionRepository = quetionRepository;
	}

	public async ValueTask<AnswerResultDto> AddAsync(AnswerCreationDto dto)
	{
		Question existQuestion = await this.quetionRepository.SelectAsync(q => q.Id.Equals(dto.QuestionId))
			?? throw new NotFoundException($"This questionId is not found {dto.QuestionId}");

		var mappedAnswer = new Answer
		{
			Text = dto.Text,
			QuestionId = dto.QuestionId,
			Question = existQuestion,
			IsTrue = dto.IsTrue
		};

        if (dto.Asset is not null)
        {
            var imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Asset }, UploadType.Images);
            var createImage = new Asset()
            {
                FileName = imageAsset.FileName,
                FilePath = imageAsset.FilePath,
            };
            mappedAnswer.AssetId = imageAsset.Id;
            mappedAnswer.Asset = createImage;
        }

        await this.repository.InsertAsync(mappedAnswer);
		await this.repository.SaveAsync();

		return this.mapper.Map<AnswerResultDto>(mappedAnswer);
	}

	public async ValueTask<AnswerResultDto> ModifyAsync(AnswerUpdateDto dto)
	{
		Answer answer = await this.repository.SelectAsync(x => x.Id.Equals(dto.Id))
			?? throw new NotFoundException($"This AnswerId is not found {dto.Id}");

        Question existQuestion = await this.quetionRepository.SelectAsync(q => q.Id.Equals(dto.QuestionId))
            ?? throw new NotFoundException($"This questionId is not found {dto.QuestionId}");

        var uploadedImage = new Asset();
        if (dto.Asset is not null)
        {
            uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Asset }, UploadType.Images);
            await this.assetService.RemoveAsync(answer.Asset);
        }

        answer.Text = dto.Text;
		answer.IsTrue = dto.IsTrue;
		answer.QuestionId = dto.QuestionId;
		answer.Question = existQuestion;

        if (uploadedImage.Id > 0)
        {
            if (answer.Asset == null)
            {
                answer.Asset = new Asset();
            }
            answer.AssetId = uploadedImage.Id;
            answer.Asset.FileName = uploadedImage.FileName;
            answer.Asset.FilePath = uploadedImage.FilePath;
        }
        this.repository.Update(answer);
		await this.repository.SaveAsync();

		return this.mapper.Map<AnswerResultDto>(answer);
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		Answer existAnswer = await this.repository.SelectAsync(x => x.Id.Equals(id))
			?? throw new NotFoundException($"This id:{id} is not found ");

		this.repository.Delete(existAnswer);
		await this.assetService.RemoveAsync(existAnswer.Asset);
		await this.repository.SaveAsync();

		return true;
	}

	public async ValueTask<AnswerResultDto> RetrieveByIdAsync(long id)
	{
		Answer answer = await repository.SelectAsync(x => x.Id.Equals(id), includes: new[] { "Question", "Asset" })
			?? throw new NotFoundException($"This id:{id} is not found ");

		return this.mapper.Map<AnswerResultDto>(answer);
	}

	public async ValueTask<IEnumerable<AnswerResultDto>> RetrieveAllAsync(PaginationParams @params)
	{
		var answers = await this.repository.SelectAll(includes: new[] { "Question", "Asset" })
			.ToPaginate(@params)
			.ToListAsync();

		return this.mapper.Map<IEnumerable<AnswerResultDto>>(answers);
	}

	public async ValueTask<IEnumerable<AnswerResultDto>> RetrieveAllByQuestionIdAsync(long questionId)
	{
		var answers = await this.repository.SelectAll(q => q.QuestionId == questionId,
			includes: new[] { "Asset" }).ToListAsync()
			?? throw new NotFoundException($"This questionId:{questionId} is not found ");

		return this.mapper.Map<IEnumerable<AnswerResultDto>>(answers);
	}
}