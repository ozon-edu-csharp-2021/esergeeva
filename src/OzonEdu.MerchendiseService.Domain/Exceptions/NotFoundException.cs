using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly string _objectName;

        public NotFoundException(string message, string objectName = null) : base(message)
        {
            _objectName = objectName;
        }

        public override string Message =>
            string.IsNullOrEmpty(_objectName) ? base.Message : $"{base.Message} ObjectName: {_objectName}";
    }
}