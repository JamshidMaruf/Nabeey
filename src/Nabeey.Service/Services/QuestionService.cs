using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class QuestionService : IQuestionService
{
    private readonly IRepository<Question> repository;
    private readonly IMapper mapper;
    public QuestionService(IRepository<Question> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async ValueTask<QuestionResultDto> AddAsync(QuestionCreationDto dto)
    {
        var question = await repository.SelectAsync(x => x.AssetId.Equals(dto.AssetId));
        if (question is not null)
            throw new AlreadyExistException("Already exist!");

        var mapQuestion = mapper.Map<Question>(dto);
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

        repository.Delete(question);
        await repository.SaveAsync();   

        return true;
    }

    public async ValueTask<QuestionResultDto> RetrieveByIdAsync(long id)
    {
        var question = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"Could not find {id}");

        var res = mapper.Map<QuestionResultDto>(question);
        return res;
    }

    public async ValueTask<IEnumerable<QuestionResultDto>> RetrieveAllAsync()
    {
        var question = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<QuestionResultDto>>(question);
        return res;
    }
}
