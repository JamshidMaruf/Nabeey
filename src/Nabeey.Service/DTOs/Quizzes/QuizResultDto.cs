﻿using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.DTOs.Quizzes;

public class QuizResultDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public int QuestionCount { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public UserResultDto User { get; set; }
	public ContentCategoryResultDto ContentCategory { get; set; }
	public byte[] Certificate { get; set; }
}