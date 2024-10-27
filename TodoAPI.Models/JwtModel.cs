using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class JwtModel
    {

        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }    
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifeTime { get; set; }

    }
}
