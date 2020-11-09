using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEntities
{
    public interface IProcessor
    {
        ResponseBase Process(Request request);
        bool CanProcess(Request request);
    }
}
