using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.Reference
{
    public class ReferenceCreateDto
    {
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
    }
}
