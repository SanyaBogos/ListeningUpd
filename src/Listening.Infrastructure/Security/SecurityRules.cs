﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Listening.Server.Security
{
    public class SecurityRules
    {
        public class User
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
        public string CertificateName { get; set; }
        public string Passwd { get; set; }
        public User[] Users { get; set; }
        public string EmailSiteName { get; set; }
        public string EmailPassword { get; set; }

        public RijndaelManaged RijndaelManaged { get; set; }
    }
}
