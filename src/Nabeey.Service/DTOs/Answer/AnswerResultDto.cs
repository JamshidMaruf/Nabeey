﻿using Nabeey.Service.DTOs.QuestionAnswer;

namespace Nabeey.Service.DTOs.Answer;

public class AnswerResultDto
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public string Text { get; set; }
    public ICollection<QuestionAnswerResultDto> QuestionAnswers { get; set; }
}
