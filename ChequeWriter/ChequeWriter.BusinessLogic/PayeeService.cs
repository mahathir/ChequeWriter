using ChequeWriter.IBusinessLogic;
using ChequeWriter.IDataAccess;
using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChequeWriter.Commons;
using ChequeWriter.Commons.Translations;

namespace ChequeWriter.BusinessLogic
{
    /// <summary>
    /// Payee Service
    /// </summary>
    /// <seealso cref="ChequeWriter.BusinessLogic.BaseService" />
    /// <seealso cref="ChequeWriter.IBusinessLogic.IPayeeService" />
    public class PayeeService : BaseService, IPayeeService
    {
        public PayeeService(IUnitOfWorks uof)
            : base(uof)
        {
        }

        /// <summary>
        /// Creates the specified payee.
        /// </summary>
        /// <param name="payee">The payee.</param>
        public IServiceResult<Payee> Create(Payee payee)
        {
            var result = new ServiceResult<Payee>();

            var existingPayee = UnitOfWork.PayeeRepo.Retrieve(payee.PayeeID);

            if (existingPayee != null)
            {
                result.ErrorMessages.Add("PayeeID", string.Format(MessagesRes._Already_, EntitiesRes.Payee,
                    CommonsRes.Exists));
                return result;
            }

            payee.Status = PayeeStatus.A.ToString();
            UnitOfWork.PayeeRepo.Create(payee);
            UnitOfWork.SaveChanges();

            result.Result = payee;
            return result;
        }

        /// <summary>
        /// Retrieves the specified Payee.
        /// </summary>
        /// <param name="id">The Payee identifier.</param>
        /// <returns>Payee</returns>
        public Payee Retrieve(long id)
        {
            return UnitOfWork.PayeeRepo.Retrieve(id);
        }

        /// <summary>
        /// Retrieves paged payee list.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="orderCriteria">The order criteria.</param>
        /// <returns></returns>
        public PagedResult<Payee> Retrieve(int pageNumber, int pageSize, 
            IDictionary<string, string> searchCriteria = null,
            IDictionary<string, string> orderCriteria = null)
        {
            return UnitOfWork.PayeeRepo.Retrieve(pageNumber, pageSize, searchCriteria, orderCriteria);
        }

        /// <summary>
        /// Updates the specified payee.
        /// </summary>
        /// <param name="payee">The payee.</param>
        public IServiceResult<bool> Update(Payee payee)
        {
            var result = new ServiceResult<bool>();

            var existingPayee = UnitOfWork.PayeeRepo.Retrieve(payee.PayeeID);

            if (existingPayee == null)
            {
                result.ErrorMessages.Add("PayeeID", string.Format(MessagesRes._NotFound, EntitiesRes.Payee));
                return result;
            }

            UnitOfWork.PayeeRepo.Update(payee);
            UnitOfWork.SaveChanges();

            result.Result = true;
            return result;
        }

        /// <summary>
        /// Deletes the specified payee.
        /// </summary>
        /// <param name="payee">The payee.</param>
        public IServiceResult<bool> Delete(Payee payee)
        {
            return Delete(payee.PayeeID);
        }

        /// <summary>
        /// Delete Payee.
        /// </summary>
        /// <param name="id">The payee id.</param>
        public IServiceResult<bool> Delete(long id)
        {
            var result = new ServiceResult<bool>();
            var payee = UnitOfWork.PayeeRepo.Retrieve(id);
            if (payee != null)
            {
                payee.Status = PayeeStatus.R.ToString();
                UnitOfWork.PayeeRepo.Update(payee);
                UnitOfWork.SaveChanges();
                result.Result = true;
            }
            return result;
        }
    }
}
