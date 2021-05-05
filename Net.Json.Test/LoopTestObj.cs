using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Json.Test
{
    class LoopTestObj
    {
        public string Name { get; set; }
        public LoopTestObj LoopProp { get; set; }
    }
}
