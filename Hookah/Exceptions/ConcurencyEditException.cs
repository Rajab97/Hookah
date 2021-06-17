using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Exceptions
{
    public class ConcurencyEditException : BaseException
    {
        public ConcurencyEditException(Exception sourceException)
            : base(ExceptionMessages.ConcurencyEdit)
        {
            this.SourceException = sourceException;
            this.ErrorCause = ExceptionMessages.ConcurencyEdit;
        }

        public Exception SourceException { get; }
    }
}
