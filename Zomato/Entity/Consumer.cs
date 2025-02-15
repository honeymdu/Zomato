﻿using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Zomato.Model
{
    public class Consumer
    {
        public long id { get; set; }
        public User user { get; set; }
        public Double rating { get; set; }
    }
}
