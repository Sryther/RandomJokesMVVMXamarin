using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Models
{
    public class ApiResponse
    {
        public string type { get; set; }
        public Joke[] value { get; set; }
    }
}