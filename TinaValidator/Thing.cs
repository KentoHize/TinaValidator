﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{    public class Thing
    {
        public Status InitalStatus { get; set; }
        public List<Sequence> Choices { get; set; } = new List<Sequence>();
    }
}
