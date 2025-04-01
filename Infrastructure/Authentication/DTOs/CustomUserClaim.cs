using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Authentication.DTOs
{
    public record CustomUserClaim(string Name = null!, string Email = null!);
}
