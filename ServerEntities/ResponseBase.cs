using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEntities
{

    public abstract class ResponseBase
    {
        public HeaderCollection Headers { get; set; }

        public string Version { get; set; }

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
    }

    public abstract class ResponseBase<T> : ResponseBase
    {
        public T Body { get; set; }

        public abstract T AppendToBody(T content);
    }
}
