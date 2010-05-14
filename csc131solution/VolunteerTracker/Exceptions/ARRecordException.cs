using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerTracker.Exceptions
{
    public enum ARRecordExceptionType { Default, NoData, NoMatchingRecords };
    public class ARRecordException : Exception
    {

        private ARRecordExceptionType exceptionType = ARRecordExceptionType.Default;
        public ARRecordExceptionType ExceptionType
        {
            get
            {
                return exceptionType;
            }
        }
        public ARRecordException(ARRecordExceptionType type)
        {
            this.exceptionType = type;
        }

    }
}
