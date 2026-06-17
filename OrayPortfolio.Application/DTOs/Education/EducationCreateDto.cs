using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.Education
{
    public class EducationCreateDto
    {
        public string? School { get; set; }
        public string? Degree { get; set; }
        public string? Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
    }
}
