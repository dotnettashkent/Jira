﻿using Jira.Domain.Commons;

namespace Jira.Domain.Entities.Users
{
    public class UserImage : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
