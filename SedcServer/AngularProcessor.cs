using ServerCore.Engine;

using ServerEntities;

using System;
using System.Collections.Generic;
using System.Text;

namespace SedcServer
{
    public class AngularProcessor : FileProcessor
    {
        public AngularProcessor(string root) : base(root)
        {

        }

        public override bool CanProcess(Request request)
        {
            return (request.Uri.Paths.Length > 1) && request.Uri.Paths[0] == "angular";
        }

        protected override string GetFileName(Request request)
        {
            return request.Uri.Paths[1];
        }
    }
}
