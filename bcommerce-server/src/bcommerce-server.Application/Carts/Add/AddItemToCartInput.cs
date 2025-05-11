using System;
using System.Collections.Generic;

namespace bcommerce_server.Application.Carts.Add;

public record AddItemToCartInput(    
    Dictionary<Guid, Dictionary<string, int>> Items
)
{
    // productId, color, quantity
}