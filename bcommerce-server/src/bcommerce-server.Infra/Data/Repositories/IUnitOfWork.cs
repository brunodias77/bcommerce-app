using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace bcommerce_server.Infra.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Inicia uma nova transação.
        /// </summary>
        Task Begin();

        /// <summary>
        /// Confirma a transação atual.
        /// </summary>
        Task Commit();

        /// <summary>
        /// Cancela a transação atual.
        /// </summary>
        Task Rollback();

        /// <summary>
        /// Conexão atual com o banco de dados.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Transação atual em execução.
        /// </summary>
        IDbTransaction Transaction { get; }
    }
}