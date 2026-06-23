using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.Skill
{
    public class SkillUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public string? Category { get; set; }
    }
}
