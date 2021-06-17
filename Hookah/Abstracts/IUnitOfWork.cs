using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Hookah.Abstracts
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        TransactionScope CreateScoppedTransaction();
    }
}
