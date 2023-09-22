
﻿using Nabeey.Domain.Commons;
﻿using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.QuestionAnswers;

namespace Nabeey.Domain.Entities.Questions;

public class Question : Auditable
{
    public string Text { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }

    public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
}
