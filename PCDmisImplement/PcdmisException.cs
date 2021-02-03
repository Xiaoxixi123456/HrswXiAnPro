using System;
using System.Runtime.Serialization;

namespace Hrsw.XiAnPro.PCDmisImplement
{
    [Serializable]
    public class PcdmisServiceException : Exception
    {
        public PcdmisServiceException()
        {
        }

        public PcdmisServiceException(string message) : base(message)
        {
        }

        public PcdmisServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PcdmisServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}