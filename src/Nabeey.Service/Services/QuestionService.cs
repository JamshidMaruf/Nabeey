using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.Exceptions;
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
        var imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.File }, UploadType.Images);

        var mapQuestion = mapper.Map<Question>(dto);
        mapQuestion.Asset = imageAsset;
        mapQuestion.AssetId = imageAsset.Id;

        await repository.InsertAsync(mapQuestion);
        await repository.SaveAsync();

        var res = mapper.Map<QuestionResultDto>(mapQuestion);
        return res;
    }

    public async ValueTask<QuestionResultDto> ModifyAsync(QuestionUpdateDto dto)
    {
        var question = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapQuestion = mapper.Map(dto, question);
        repository.Update(mapQuestion);
        await repository.SaveAsync();

        var res = mapper.Map<QuestionResultDto>(mapQuestion);
        return res;
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var question = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        this.repository.Delete(question);
        await this.assetService.RemoveAsync(question.Asset);
        await this.repository.SaveAsync();   

        return true;
    }

    public async ValueTask<QuestionResultDto> RetrieveByIdAsync(long id)
    {
        var question = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"Could not find {id}");

        var res = this.mapper.Map<QuestionResultDto>(question);
        return res;
    }

    public async ValueTask<IEnumerable<QuestionResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var question = await repository.SelectAll().ToListAsync();
        var res = this.mapper.Map<IEnumerable<QuestionResultDto>>(question);
        return res;
    }
}
