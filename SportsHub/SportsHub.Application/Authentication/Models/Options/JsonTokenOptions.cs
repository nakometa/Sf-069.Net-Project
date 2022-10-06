using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.AppService.Authentication.Models.Options
{
    public class JsonTokenOptions
    {
        public const string Jwt = "Jwt";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
