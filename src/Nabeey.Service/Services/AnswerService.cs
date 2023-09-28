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

		var imageAsset = new Asset();
		if (dto.Asset != null)
		{
			imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Asset }, UploadType.Images);
		}

		Answer mapAnswer = mapper.Map<Answer>(dto);
		mapAnswer.Asset = imageAsset;
		mapAnswer.AssetId = imageAsset.Id;

		await this.repository.InsertAsync(mapAnswer);
		await this.repository.SaveAsync();

		return this.mapper.Map<AnswerResultDto>(mapAnswer);
	}

	public async ValueTask<AnswerResultDto> ModifyAsync(AnswerUpdateDto dto)
	{
		Answer answer = await this.repository.SelectAsync(x => x.Id.Equals(dto.Id))
			?? throw new NotFoundException($"This AnswerId is not found {dto.Id}");

		Answer mapAnswer = this.mapper.Map(dto, answer);
		this.repository.Update(mapAnswer);
		await this.repository.SaveAsync();

		return this.mapper.Map<AnswerResultDto>(mapAnswer);
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
		Answer answer = await repository.SelectAsync(x => x.Id.Equals(id))
			?? throw new NotFoundException($"This id:{id} is not found ");

		return this.mapper.Map<AnswerResultDto>(answer);
	}

	public async ValueTask<IEnumerable<AnswerResultDto>> RetrieveAllAsync(PaginationParams @params)
	{
		var answers = await this.repository.SelectAll()
			.ToPaginate(@params)
			.ToListAsync();

		return this.mapper.Map<IEnumerable<AnswerResultDto>>(answers);
	}

	public async ValueTask<IEnumerable<AnswerResultDto>> RetrieveAllByQuestionIdAsync(long questionId)
	{
		var answers = await this.repository.SelectAll().Where(q => q.QuestionId == questionId).ToListAsync()
			?? throw new NotFoundException($"This quetionId:{questionId} is not found ");

		return this.mapper.Map<IEnumerable<AnswerResultDto>>(answers);
	}
}