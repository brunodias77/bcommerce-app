using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcommerce_server.Application.Abstractions;

public readonly struct Unit
{
    public static readonly Unit Value = new();
}