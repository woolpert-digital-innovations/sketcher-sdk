using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Demo.Models
{
        public class Root
        {
            public List<Sketch> sketches { get; set; }
        }

        public class Sketch
        {
            public int id { get; set; }
            public string name { get; set; }
            public int page { get; set; }
            public string data { get; set; }
        }
}
