using System;
using System.Collections.Generic;

namespace bcommerce_server.Application.Carts.Add;

public record AddItemToCartInput(
    Guid ProductId,
    string Color,
    int Quantity
);