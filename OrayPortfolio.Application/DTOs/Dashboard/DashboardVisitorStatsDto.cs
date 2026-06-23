using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.Dashboard
{
    public class DashboardVisitorStatsDto
    {
        public int TodayVisitors { get; set; }
        public int TotalVisitors { get; set; }
        public List<string> WeeklyVisitorDates { get; set; } = new();
        public List<int> WeeklyVisitorCounts { get; set; } = new();
    }
}