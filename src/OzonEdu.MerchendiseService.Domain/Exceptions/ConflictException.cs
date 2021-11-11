using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions
{
    public class ConflictException: Exception
    {
        private readonly string _objectName;

        public ConflictException(string message, string objectName = null) : base(message)
        {
            _objectName = objectName;
        }

        public override string Message =>
            string.IsNullOrEmpty(_objectName) ? base.Message : $"{base.Message} ObjectName: {_objectName}";
    }
}