using ChequeWriter.Commons.Translations;
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

        internal static void ValidateNullWhiteSpace<TEntity>(string value, ServiceResult<TEntity> result, 
            string prop, string entity)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.ErrorMessages.Add(prop, string.Format(MessagesRes._CantBe_, entity,
                    CommonsRes.Empty));
            }
        }
    }
}
