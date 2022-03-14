using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Exceptions
{
    public class HttpException:Exception
    {
        internal HttpException(int errorCode, string errorMessage):base(errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
        public int ErrorCode { get; }
        public string ErrorMessage { get; }
    }
}
