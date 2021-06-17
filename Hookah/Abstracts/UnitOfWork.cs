using Hookah.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Hookah.Abstracts
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AppDbContext _context;
        private TransactionScope transactionScope;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public TransactionScope CreateScoppedTransaction()
        {
            if (transactionScope == null)
            {
                transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            }
            return transactionScope;
        }
        public async Task<int> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (transactionScope != null)
            {
                transactionScope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            if (transactionScope != null)
            {
                transactionScope.Dispose();
            }
        }
    }
}
