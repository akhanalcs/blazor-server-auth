using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Models.Exceptions
{
    public class SomeHMTIssueException : Exception
    {
        public SomeHMTIssueException(string message)
        : base($"Some message about this issue: \"{message}\".")
        {
        }
    }
}
