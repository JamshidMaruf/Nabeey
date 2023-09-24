using AutoMapper;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentCategories;
namespace Nabeey.Service.Mappers;

using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.ContentAudio;
using Nabeey.Service.DTOs.ContentImages;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.Users;

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

		// ContentVideo
		CreateMap<ContentVideo, ContentVideoResultDto>().ReverseMap();
		CreateMap<ContentVideoCreationDto, ContentVideo>().ReverseMap();
        CreateMap<ContentVideoUpdateDto, ContentVideo>().ReverseMap();

		// ContentAudio
		CreateMap<ContentAudio, ContentCategoryResultDto>().ReverseMap();
        CreateMap<ContentAudioCreationDto, ContentCategory>().ReverseMap();
        CreateMap<ContentAudioUpdateDto, ContentCategory>().ReverseMap();

		//User
		CreateMap<User,UserCreationDto>().ReverseMap();
		CreateMap<User,UserUpdateDto>().ReverseMap();
		CreateMap<User,UserResultDto>().ReverseMap();
    }
}