﻿using Microsoft.AspNetCore.Http;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Service.DTOs.Answers;

public class AnswerCreationDto
{
	public string Text { get; set; }
	public long QuestionId { get; set; }
	public IFormFile? Asset { get; set; }
    public bool IsTrue { get; set; }
}
