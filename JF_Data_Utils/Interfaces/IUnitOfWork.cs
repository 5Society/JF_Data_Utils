using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        public IDbContextTransaction GetCurrentTransaction();
        bool HasActiveTransaction { get; }
        void DetectChanges(); 
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        bool CommitTransaction();
        Task<bool> CommitTransactionAsync();
        void RollbackTransaction();

    }
}
