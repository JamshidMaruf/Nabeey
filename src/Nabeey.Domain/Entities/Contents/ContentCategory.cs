﻿using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentCategory : Auditable
{
    public string Name { get; set; }
    public ICollection<Content> Contents { get; set; }
}