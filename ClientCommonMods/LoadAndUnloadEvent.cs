﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommonMods
{

    public class LoadEvent : PubSubEvent<bool>
    { }

    public class UnloadEvent : PubSubEvent<bool>
    { }
}
