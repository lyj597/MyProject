using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole
{
    [AttributeUsage(AttributeTargets.Class)]
   sealed  class MyAttribute:Attribute
    {
        public string Description { get; set; }

        public string Length { get; set; }
    }
}
