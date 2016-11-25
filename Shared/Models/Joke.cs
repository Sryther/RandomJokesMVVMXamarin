using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Models
{
    public class Joke
    {
        public int id { get; set; }
        public string joke { get; set; }
        public string[] categories { get; set; }
    }
}