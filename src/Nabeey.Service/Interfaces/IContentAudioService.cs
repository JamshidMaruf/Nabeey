﻿using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentAudio;

namespace Nabeey.Service.Interfaces;

public interface IContentAudioService
{
    Task<ContentAudio> UploadAsync(ContentAudioCreationDto dto);
    Task<bool> RemoveAsync(ContentAudio dto);
}