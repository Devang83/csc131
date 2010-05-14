using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerTracker.Exceptions
{
    public enum RemitInfoExceptionType { Default, NoData };
    public class RemitInfoException : Exception
    {

        private RemitInfoExceptionType exceptionType = RemitInfoExceptionType.Default;
        public RemitInfoExceptionType ExceptionType
        {
            get
            {
                return exceptionType;
            }
        }
        public RemitInfoException(RemitInfoExceptionType type)
        {
            this.exceptionType = type;
        }

    }
}
