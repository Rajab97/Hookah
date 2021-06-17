using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Exceptions
{
    public class FatalException : BaseException
    {
        public FatalException(Exception sourceException)
            : base(ExceptionMessages.FatalError)
        {
            this.SourceException = sourceException;
            this.ErrorCause = ExceptionMessages.FatalError;
        }

        public Exception SourceException { get; }
    }
}
