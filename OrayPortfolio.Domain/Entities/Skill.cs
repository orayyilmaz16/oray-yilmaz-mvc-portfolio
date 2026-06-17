using OrayPortfolio.Domain.Common;

namespace OrayPortfolio.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public string? Name { get; set; }
        public int? Level { get; set; } // 0-100
        public string? Category { get; set; } // Backend, Frontend, DevOps vb.
    }
}
