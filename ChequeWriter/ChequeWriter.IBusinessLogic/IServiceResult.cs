using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.IBusinessLogic
{
    public interface IServiceResult<TEntity>
    {
        TEntity Result { get; }
        IDictionary<string, string> ErrorMessages { get; }
        string ErrorSummary { get; }
    }
}
