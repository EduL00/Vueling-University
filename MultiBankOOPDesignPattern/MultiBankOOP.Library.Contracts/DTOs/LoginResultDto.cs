using MultiBankOOP.XCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiBankOOP.Library.Contracts.DTOs
{
    public class LoginResultDto
    {
        public bool ResultHasErrors;
        public LoginErrorEnum? Error;
        public int ? UserId;
    }
}
