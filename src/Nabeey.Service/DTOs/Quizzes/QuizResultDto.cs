﻿using Nabeey.Service.DTOs.Users;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.DTOs.Quizzes;

public class QuizResultDto
{
	public string Name { get; set; }
	public string Description { get; set; }
	public int QuestionCount { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public UserResultDto User { get; set; }
	public ContentCategoryResultDto ContentCategory { get; set; }
}