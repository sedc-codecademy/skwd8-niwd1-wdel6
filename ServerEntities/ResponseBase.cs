using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEntities
{

    public abstract class ResponseBase
    {
        public HeaderCollection Headers { get; set; } = new HeaderCollection();

        public string Version { get; set; } = "1.1";

        private int status;

        public StatusCode Status
        {
            get => (StatusCode)status;
            set
            {
                status = (int)value;
                Message = value.GetDescription();
            }
        }

        public string Message { get; set; }

        public abstract byte[] AppendBody(byte[] content);
    }

    public abstract class ResponseBase<T> : ResponseBase
    {
        public T Body { get; set; }
    }
}
