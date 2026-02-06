using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserAccount user);
    }
}
