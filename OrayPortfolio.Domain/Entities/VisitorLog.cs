using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Domain.Entities
{
    public class VisitorLog
    {
        public int Id { get; set; }
        public string? IpAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }
}