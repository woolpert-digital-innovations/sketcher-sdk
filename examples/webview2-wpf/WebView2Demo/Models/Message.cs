using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebView2Demo.Models
{
    public class Message
    {
        public string Type { get; set; }
        public Newtonsoft.Json.Linq.JObject Data { get; set; }
    }
}
