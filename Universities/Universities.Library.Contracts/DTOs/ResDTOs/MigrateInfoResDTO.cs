using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universities.XCutting.Enums;

namespace Universities.Library.Contracts.DTOs.ResDTOs
{
    public class MigrateInfoResDTO
    {
        public bool HasError { get; set; }
        public MigrateInfoResErrorEnum? Error { get; set; }
    }
}
