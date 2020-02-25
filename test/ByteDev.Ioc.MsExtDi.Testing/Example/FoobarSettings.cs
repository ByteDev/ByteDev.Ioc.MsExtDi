using System.Collections.Generic;

namespace ByteDev.Ioc.MsExtDi.Testing.Example
{
    public class FoobarSettings
    {
        public string FoobarUrl { get; set; }

        public Bar Bar { get; set; }

        public IList<Bar> Bars { get; set; }
    }

    public class Bar
    {
        public string Name { get; set; }
    }
}