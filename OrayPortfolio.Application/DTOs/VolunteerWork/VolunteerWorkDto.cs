using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.VolunteerWork
{
    public class VolunteerWorkDto
    {
        public int Id { get; set; }
        public string? Organization { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
