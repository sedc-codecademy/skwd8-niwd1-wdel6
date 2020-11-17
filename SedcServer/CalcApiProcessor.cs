using SedcJsonSerializer;
using ServerCore.Responses;
using ServerEntities;

using System.Collections.Generic;
using System.Linq;


namespace SedcServer
{
    class CalcApiProcessor: IProcessor
    {
        private bool IsValidOperation(string operation, double second)
        {
            var operations = new List<string> { "add", "sub", "mul", "div" };

            if (operation == "div" && second == 0) return false;
            if (!operations.Contains(operation)) return false;

            return true;
        }

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
            var parseResult = double.TryParse(firstValue, out double first); 

            if (parseResult is false)
            {
                return ResponseGenerator.MakeSemanticErrorResponse("The first value is not a number");
            }

            var secondValue = arguments.Skip(2).First();
            parseResult = double.TryParse(secondValue, out double second);

            if (parseResult is false)
            {
                return ResponseGenerator.MakeSemanticErrorResponse("The first value is not a number");
            }

            if (!IsValidOperation(operation, second))
            {
                return ResponseGenerator.MakeSemanticErrorResponse("Invalid argument input, please check the operation and the arguments.\nEx: add/1/2");
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
