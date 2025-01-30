using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Shared.Enums
{
    public enum StatusResponseEnum
    {
        Success = 1,
        ServerFailure = 2,
        I4ProFailure = 3,
        BusinessRuleFailure = 4,
        AuthorizationFailure = 5
    }
}