using ChequeWriter.IBusinessLogic;
using ChequeWriter.IDataAccess;
using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChequeWriter.Commons;

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
        public void Create(Payee payee)
        {
            payee.Status = PayeeStatus.A.ToString();
            UnitOfWork.PayeeRepo.Create(payee);
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
            IList<string> orderCriteria = null)
        {
            return UnitOfWork.PayeeRepo.Retrieve(pageNumber, pageSize, searchCriteria, orderCriteria);
        }

        /// <summary>
        /// Updates the specified payee.
        /// </summary>
        /// <param name="payee">The payee.</param>
        public void Update(Payee payee)
        {
            UnitOfWork.PayeeRepo.Update(payee);
        }

        /// <summary>
        /// Deletes the specified payee.
        /// </summary>
        /// <param name="payee">The payee.</param>
        public void Delete(Payee payee)
        {
            var existingPayee = UnitOfWork.PayeeRepo.Retrieve(payee.CustomerID);
            if (existingPayee != null)
            {
                existingPayee.Status = PayeeStatus.R.ToString();
                UnitOfWork.PayeeRepo.Update(existingPayee);
            }
        }

        /// <summary>
        /// Delete Payee.
        /// </summary>
        /// <param name="id">The payee id.</param>
        public void Delete(long id)
        {
            var payee = UnitOfWork.PayeeRepo.Retrieve(id);
            if (payee != null)
            {
                payee.Status = PayeeStatus.R.ToString();
                UnitOfWork.PayeeRepo.Update(payee);
            }
        }
    }
}
