using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace bcommerce_server.Infra.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit(); // Confirma transação
        Task Rollback(); // 🆕 Adicionado para controle
        NpgsqlConnection Connection { get; }
        NpgsqlTransaction Transaction { get; }
    }
}