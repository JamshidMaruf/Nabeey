using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class QuestionService : IQuestionService
{
	private readonly IRepository<Question> repository;
	private readonly IAssetService assetService;
	private readonly IMapper mapper;
	public QuestionService(IRepository<Question> repository, IMapper mapper, IAssetService assetService)
	{
		this.mapper = mapper;
		this.repository = repository;
		this.assetService = assetService;
	}
	public async ValueTask<QuestionResultDto> AddAsync(QuestionCreationDto dto)
	{
		var mapQuestion = new Question
		{
			Text = dto.Text,
		};

		if (dto.Image is not null)
		{
			var imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
			var createImage = new Asset()
			{
				FileName = imageAsset.FileName,
				FilePath = imageAsset.FilePath,
			};
		    mapQuestion.ImageId = imageAsset.Id;
			mapQuestion.Image = createImage;
		}

		await repository.InsertAsync(mapQuestion);
		await repository.SaveAsync();

		return mapper.Map<QuestionResultDto>(mapQuestion);
	}

	public async ValueTask<QuestionResultDto> ModifyAsync(QuestionUpdateDto dto)
	{
		var question = await repository.SelectAsync(x => x.Id.Equals(dto.Id), includes: new[] {"Image"})
			?? throw new NotFoundException("Not found!");

        var uploadedImage = new Asset();
        if (dto.Image is not null)
        {
            uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
            await this.assetService.RemoveAsync(question.Image);
        }

        question.Text = dto.Text;

        if (uploadedImage.Id > 0)
        {
            if (question.Image == null)
            {
                question.Image = new Asset();
            }
            question.ImageId = uploadedImage.Id;
            question.Image.FileName = uploadedImage.FileName;
            question.Image.FilePath = uploadedImage.FilePath;
        }

        this.repository.Update(question);
		await this.repository.SaveAsync();
		var res = mapper.Map<QuestionResultDto>(question);
		return res;
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		var question = await repository.SelectAsync(x => x.Id.Equals(id), includes: new[] { "Image" })
			?? throw new NotFoundException("Not found!");

		this.repository.Delete(question);
		await this.assetService.RemoveAsync(question.Image);
		await this.repository.SaveAsync();

		return true;
	}

	public async ValueTask<QuestionResultDto> RetrieveByIdAsync(long id)
	{
		var question = await repository.SelectAsync(x => x.Id.Equals(id), includes: new[] { "Answers", "Image" })
			?? throw new NotFoundException($"Could not find {id}");

		var res = this.mapper.Map<QuestionResultDto>(question);
		return res;
	}

	public async ValueTask<IEnumerable<QuestionResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
	{
		var questions = await repository
		  .SelectAll()
		  .Include(q => q.Answers)
		  .Include(q => q.Image)
		  .ToPaginate(@params)
		  .ToListAsync();

		if (search is not null)
		{
			questions = questions.Where(d => d.Text.Contains(search,
				StringComparison.OrdinalIgnoreCase)).ToList();
		}

		return this.mapper.Map<IEnumerable<QuestionResultDto>>(questions);
	}
}