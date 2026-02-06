using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.AuthLogIn.DTOs
{
    public class AuthResponseDto<T>
    {
        public BaseResult? BaseResult { get; set; }
        public T? Data { get; set; }
    }
    public class BaseResult
    {
        public string? Code { get; set; }
        public string? Status { get; set; }
        public string? MsgCode { get; set; }
        public string? Msg { get; set; }
        public string? Ref { get; set; }
        public bool firstLogin { get; set; }
    }
}