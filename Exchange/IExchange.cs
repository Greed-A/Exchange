﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange
{
    public interface IExchange
    {
        Task StartWebSocketAsync();
        string GetName();
    }
}