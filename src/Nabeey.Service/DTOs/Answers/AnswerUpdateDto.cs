﻿using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Answers;

public class AnswerUpdateDto
{
    public long Id { get; set; }
	public string Text { get; set; }
	public IFormFile File { get; set; }
	public long QuestionId { get; set; }
}
