using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardev.Arq.Services.Identity.Business.Models
{
    public class LoginResult
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
    }
}
