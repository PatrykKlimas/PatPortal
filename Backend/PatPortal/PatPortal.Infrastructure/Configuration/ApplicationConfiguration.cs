﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Infrastructure.Configuration
{
    public class ApplicationConfiguration
    {
        public bool UseMockRepositories { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
    public class ConnectionStrings
    {
        public string PatPortalDataBase { get; set; }
    }
}
