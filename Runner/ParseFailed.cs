using System;

namespace AoC.Runner
{
    public class ParseFailed : Exception
    {
        public ParseFailed(string message)
            : base(message)
        {
        }
    }
}
