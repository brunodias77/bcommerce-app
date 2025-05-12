using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Products.GetAll;

public interface IGetAllProuctsUseCase : IUseCase<Unit, GetAllProductItemOutput, Notification>
{
}
