using ChequeWriter.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.BusinessLogic
{
    public abstract class BaseService
    {
        internal IUnitOfWorks UnitOfWork;

        public BaseService(IUnitOfWorks uof)
        {
            UnitOfWork = uof;
        }
    }
}
