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
    /// Cheque Service
    /// </summary>
    /// <seealso cref="ChequeWriter.BusinessLogic.BaseService" />
    /// <seealso cref="ChequeWriter.IBusinessLogic.IChequeService" />
    public class ChequeService : BaseService, IChequeService
    {
        public ChequeService(IUnitOfWorks uof)
            : base(uof)
        {
        }

        #region IChequeService

        /// <summary>
        /// Creates the specified cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public void Create(Cheque cheque)
        {
            cheque.Status = ChequeStatus.A.ToString();
            UnitOfWork.ChequeRepo.Create(cheque);
        }

        /// <summary>
        /// Retrieves the specified Cheque.
        /// </summary>
        /// <param name="id">The Cheque identifier.</param>
        /// <returns>Cheque</returns>
        public Cheque Retrieve(long id)
        {
            return UnitOfWork.ChequeRepo.Retrieve(id);
        }

        /// <summary>
        /// Retrieves paged cheque list.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="orderCriteria">The order criteria.</param>
        /// <returns></returns>
        public PagedResult<Cheque> Retrieve(int pageNumber, int pageSize,
            IDictionary<string, string> searchCriteria = null,
            IList<string> orderCriteria = null)
        {
            return UnitOfWork.ChequeRepo.Retrieve(pageNumber, pageSize, searchCriteria, orderCriteria);
        }

        /// <summary>
        /// Updates the specified cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public void Update(Cheque cheque)
        {
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.CustomerID);

            // Trying to change status.
            if (existingCheque.Status != cheque.Status)
            {
                // Trying to delete cheque.
                if (cheque.Status == ChequeStatus.R.ToString())
                {
                    // Validate delete request.
                    if (!ValidateDeleteCheque(existingCheque)) return;
                }
                // Trying to activate cheque.
                else if (cheque.Status == ChequeStatus.A.ToString())
                {
                    // Can't reactivated cheque.
                    return;
                }
                // Trying to cancel cheque.
                else if (cheque.Status == ChequeStatus.C.ToString())
                {
                    // Only printed cheque that can be canceled.
                    if (!ValidateCancelCheque(existingCheque)) return;
                }
                // Trying to print cheque.
                else if (cheque.Status == ChequeStatus.P.ToString())
                {
                    // Only active cheque that can be printed.
                    if (!ValidatePrintCheque(existingCheque)) return;
                }
            }
            UnitOfWork.ChequeRepo.Update(cheque);
        }

        /// <summary>
        /// Deletes the specified cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public void Delete(Cheque cheque)
        {
            Delete(cheque.CustomerID);
        }

        /// <summary>
        /// Delete Cheque.
        /// </summary>
        /// <param name="id">The cheque id.</param>
        public void Delete(long id)
        {
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(id);
            if (existingCheque != null && ValidateDeleteCheque(existingCheque))
            {
                existingCheque.Status = ChequeStatus.R.ToString();
                UnitOfWork.ChequeRepo.Update(existingCheque);
            }
        }

        /// <summary>
        /// Cancels the cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public void CancelCheque(Cheque cheque)
        {
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.ChequeID);
            if (existingCheque != null && ValidateCancelCheque(existingCheque))
            {
                existingCheque.Status = ChequeStatus.C.ToString();
                UnitOfWork.ChequeRepo.Update(existingCheque);
            }
        }

        /// <summary>
        /// Prints the cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public void PrintCheque(Cheque cheque)
        {
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.ChequeID);
            if (existingCheque != null && ValidatePrintCheque(existingCheque))
            {
                existingCheque.Status = ChequeStatus.C.ToString();
                UnitOfWork.ChequeRepo.Update(existingCheque);
            }
        }

        /// <summary>
        /// Generates the cheque number.
        /// </summary>
        /// <returns></returns>
        public string GenerateChequeNumber()
        {
            return DateTime.UtcNow.ToString("yyyy-MMdd-HHmm-ss");
        }

        /// <summary>
        /// Gets the amount words.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetAmountWords(decimal amount)
        {
            throw new NotImplementedException();
        }

        #endregion IChequeService

        #region Private Methods        
        /// <summary>
        /// Validates the delete cheque.
        /// </summary>
        /// <param name="existingCheque">The existing cheque.</param>
        /// <returns></returns>
        private bool ValidateDeleteCheque(Cheque existingCheque)
        {
            // Can't delete printed cheque.
            // Can't delete canceled cheque.
            return existingCheque.Status != ChequeStatus.P.ToString()
                && existingCheque.Status != ChequeStatus.C.ToString();
        }

        /// <summary>
        /// Validates the cancel cheque.
        /// </summary>
        /// <param name="existingCheque">The existing cheque.</param>
        /// <returns></returns>
        private bool ValidateCancelCheque(Cheque existingCheque)
        {
            // Only printed cheque that can be canceled.
            return existingCheque.Status == ChequeStatus.P.ToString();
        }

        /// <summary>
        /// Validates the print cheque.
        /// </summary>
        /// <param name="existingCheque">The existing cheque.</param>
        /// <returns></returns>
        private bool ValidatePrintCheque(Cheque existingCheque)
        {
            return existingCheque.Status == ChequeStatus.A.ToString();
        }
        #endregion Private Methods
    }
}
