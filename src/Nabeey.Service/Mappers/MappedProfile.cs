using AutoMapper;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentCategories;
namespace Nabeey.Service.Mappers;
using Nabeey.Service.DTOs.ContentImages;

public class MappedProfile : Profile
{
    public MappedProfile()
    {
		// ContentCategory
		CreateMap<ContentCategory, ContentCategoryCreationDto>().ReverseMap();
		CreateMap<ContentCategory, ContentCategoryUpdateDto>().ReverseMap();
		CreateMap<ContentCategory, ContentCategoryResultDto>().ReverseMap();

		// ContentImage
		CreateMap<ContentImage, ContentImageCreationDto>().ReverseMap();
		CreateMap<ContentImage, ContentImageUpdateDto>().ReverseMap();
		CreateMap<ContentImage, ContentImageResultDto>().ReverseMap();
	}
}
