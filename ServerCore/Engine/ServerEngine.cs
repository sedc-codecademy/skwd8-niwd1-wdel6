using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore.Engine
{
    public class ServerEngine
    {
        private List<IProcessor> processors;

        public ServerEngine()
        {
            processors = new List<IProcessor>
            {
                new CalculationProcessor(),
                new FileProcessor(),
                new EchoProcessor()
            };
        }

        public ResponseBase Process(Request request)
        {
            foreach (var processor in processors)
            {
                if (processor.CanProcess(request))
                {
                    var calcResponse = processor.Process(request);
                    return calcResponse;
                }
            }

            throw new ApplicationException("This is not possible");
        }

        internal void RegisterProcessor(IProcessor processor)
        {
            processors.Insert(0, processor);
        }
    }
}
