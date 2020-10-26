using System;
using System.Collections.Generic;
using System.Text;

namespace RegexDemo
{
    class RegexReader : IReader
    {
        public IEnumerable<NanoBot> ReadNanobots(IEnumerable<string> nanobotLines)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Star> ReadStars(IEnumerable<string> starLines)
        {
            throw new NotImplementedException();
        }
    }
}
