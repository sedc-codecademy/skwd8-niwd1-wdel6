using SedcJsonSerializer;

using ServerCore.Responses;

using ServerEntities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SedcServer
{
    class CalcApiProcessor: IProcessor
    {
        public bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length >= 1) && (request.Uri.Paths[0] == "calc-api");
        }

        public ResponseBase Process(Request request)
        {
            var arguments = request.Uri.Paths.Skip(1).Take(3);
            if (arguments.Count() != 3)
            {
                return ResponseGenerator.MakeSemanticErrorResponse("Not enough arguments");
            }

            var operation = arguments.First();
            var firstValue = arguments.Skip(1).First();
            var parseResult = int.TryParse(firstValue, out int first); 

            if (parseResult is false)
            {
                return ResponseGenerator.MakeSemanticErrorResponse("The first value is not a number");
            }

            var secondValue = arguments.Skip(2).First();
            parseResult = int.TryParse(secondValue, out int second);

            if (parseResult is false)
            {
                return ResponseGenerator.MakeSemanticErrorResponse("The first value is not a number");
            }

            var calc = new Calculator();
            var result = calc.Calculate(operation, first, second);

            var body = Serializer.Serialize(result);

            return new Response
            {
                Body = body,
                Headers = new HeaderCollection
                {
                    { "Content-Type", "application/json" }
                },
                Status = StatusCode.OK
            };
        }
    }
}
