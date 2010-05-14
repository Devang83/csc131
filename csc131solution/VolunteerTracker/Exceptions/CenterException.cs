using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerTracker.Exceptions
{
    public enum CenterExceptionType { Default, NoData };
    public class CenterException : Exception
    {

        private CenterExceptionType exceptionType = CenterExceptionType.Default;
        public CenterExceptionType ExceptionType
        {
            get
            {
                return exceptionType;
            }
        }
        public CenterException(CenterExceptionType type)
        {
            this.exceptionType = type;
        }

    }    
}
