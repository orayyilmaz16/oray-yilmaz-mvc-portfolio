using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.Auth
{
    public class LoginViewModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Code { get; set; }
    }
}