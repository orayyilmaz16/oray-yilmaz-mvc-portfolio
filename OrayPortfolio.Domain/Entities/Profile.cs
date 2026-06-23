using OrayPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Domain.Entities
{
    public class Profile: BaseEntity
    {
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? ShortBio { get; set; }
        public string? LongBio { get; set; }
        public string? ProfileImageUrl { get; set; }

        public string? GithubUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? InstagramUrl { get; set; }

        public string? CvFilePath { get; set; }
        public string? Email { get; set; }
    }
    
}
