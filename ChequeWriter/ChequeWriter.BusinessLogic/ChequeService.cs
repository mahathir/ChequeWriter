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
using System.Globalization;

namespace ChequeWriter.BusinessLogic
{
    /// <summary>
    /// Cheque Service
    /// </summary>
    /// <seealso cref="ChequeWriter.BusinessLogic.BaseService" />
    /// <seealso cref="ChequeWriter.IBusinessLogic.IChequeService" />
    public class ChequeService : BaseService, IChequeService
    {
        private string _chequeNoFormat = "yyyy-MMdd-HHmm-ss";
        private const decimal BILLION = 1000000000;
        private const decimal MILLION = 1000000;
        private const decimal THOUSANDS = 1000;
        private const decimal HUNDREDS = 100;
        private Dictionary<decimal, string> textStrings = new Dictionary<decimal, string>();
        private Dictionary<decimal, string> scales = new Dictionary<decimal, string>(); 
        public ChequeService(IUnitOfWorks uof)
            : base(uof)
        {
            Initialize();
        }

        #region IChequeService

        /// <summary>
        /// Creates the specified cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public IServiceResult<Cheque> Create(Cheque cheque)
        {
            var result = new ServiceResult<Cheque>();

            if (string.IsNullOrWhiteSpace(cheque.ChequeNo))
            {
                cheque.PostingDate = DateTime.UtcNow;
                cheque.ChequeNo = GenerateChequeNumber(cheque.PostingDate);
            }
            else
            {
                cheque.PostingDate = DateTime.ParseExact(cheque.ChequeNo, _chequeNoFormat,
                    CultureInfo.InvariantCulture);
            }

            var customer = UnitOfWork.CustomerRepo.Retrieve(cheque.CustomerID);

            if (customer == null)
            {
                result.ErrorMessages.Add("CustomerID", string.Format(MessagesRes._NotFound, EntitiesRes.Customer));
                return result;
            }

            var payee = UnitOfWork.PayeeRepo.Retrieve(cheque.PayeeID);

            if (payee == null)
            {
                result.ErrorMessages.Add("PayeeID", string.Format(MessagesRes._NotFound, EntitiesRes.Payee));
                return result;
            }

            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.ChequeID);

            if (existingCheque != null)
            {
                result.ErrorMessages.Add("ChequeID", string.Format(MessagesRes._Already_, EntitiesRes.Cheque,
                    CommonsRes.Exists));
                return result;
            }

            var pagedResult = UnitOfWork.ChequeRepo.Retrieve(1, 1,
                new Dictionary<string, string> { { "ChequeNo", cheque.ChequeNo } });

            if (pagedResult.TotalCount > 0)
            {
                result.ErrorMessages.Add("ChequeNo", string.Format(MessagesRes._Already_, EntitiesRes.ChequeNo,
                    CommonsRes.Exists));
                return result;
            }

            cheque.Status = ChequeStatus.A.ToString();
            UnitOfWork.ChequeRepo.Create(cheque);
            UnitOfWork.SaveChanges();

            result.Result = cheque;
            return result;
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
        public IServiceResult<bool> Update(Cheque cheque)
        {
            var result = new ServiceResult<bool>();
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.ChequeID);

            if (existingCheque == null)
            {
                result.ErrorMessages.Add("ChequeID", string.Format(MessagesRes._NotFound, EntitiesRes.Cheque));
                result.Result = false;
                return result;
            }

            // Trying to change status.
            if (existingCheque.Status != cheque.Status)
            {
                // Trying to delete cheque.
                if (cheque.Status == ChequeStatus.R.ToString())
                {
                    // Validate delete request.
                    if (!ValidateDeleteCheque(existingCheque, result)) return result;
                }
                // Trying to activate cheque.
                else if (cheque.Status == ChequeStatus.A.ToString())
                {
                    // Can't reactivated cheque.
                    result.ErrorMessages["Status"] = string.Format(MessagesRes.Cant__, CommonsRes.Reactivate,
                        EntitiesRes.Cheque);
                    return result;
                }
                // Trying to cancel cheque.
                else if (cheque.Status == ChequeStatus.C.ToString())
                {
                    // Only printed cheque that can be canceled.
                    if (!ValidateCancelCheque(existingCheque, result)) return result;
                }
                // Trying to print cheque.
                else if (cheque.Status == ChequeStatus.P.ToString())
                {
                    // Only active cheque that can be printed.
                    if (!ValidatePrintCheque(existingCheque, result)) return result;
                }
            }
            UnitOfWork.ChequeRepo.Update(cheque);
            UnitOfWork.SaveChanges();
            result.Result = true;

            return result;
        }

        /// <summary>
        /// Deletes the specified cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public IServiceResult<bool> Delete(Cheque cheque)
        {
            return Delete(cheque.CustomerID);
        }

        /// <summary>
        /// Delete Cheque.
        /// </summary>
        /// <param name="id">The cheque id.</param>
        public IServiceResult<bool> Delete(long id)
        {
            var result = new ServiceResult<bool>();
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(id);
            if (existingCheque != null && ValidateDeleteCheque(existingCheque, result))
            {
                existingCheque.Status = ChequeStatus.R.ToString();
                UnitOfWork.ChequeRepo.Update(existingCheque);
                UnitOfWork.SaveChanges();
                result.Result = true;
            }
            return result;
        }

        /// <summary>
        /// Cancels the cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public IServiceResult<bool> CancelCheque(Cheque cheque)
        {
            var result = new ServiceResult<bool>();
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.ChequeID);
            if (existingCheque != null && ValidateCancelCheque(existingCheque, result))
            {
                existingCheque.Status = ChequeStatus.C.ToString();
                UnitOfWork.ChequeRepo.Update(existingCheque);
                UnitOfWork.SaveChanges();
                result.Result = true;
            }
            return result;
        }

        /// <summary>
        /// Prints the cheque.
        /// </summary>
        /// <param name="cheque">The cheque.</param>
        public IServiceResult<bool> PrintCheque(Cheque cheque)
        {
            var result = new ServiceResult<bool>();
            var existingCheque = UnitOfWork.ChequeRepo.Retrieve(cheque.ChequeID);
            if (existingCheque != null && ValidatePrintCheque(existingCheque, result))
            {
                existingCheque.Status = ChequeStatus.C.ToString();
                UnitOfWork.ChequeRepo.Update(existingCheque);
                UnitOfWork.SaveChanges();
                result.Result = true;
            }
            return result;
        }


        /// <summary>
        /// Generates the cheque number.
        /// </summary>
        /// <param name="datetime">datetime</param>
        /// <returns>the cheque number.</returns>
        public string GenerateChequeNumber(DateTime? datetime = null)
        {
            return (datetime ?? DateTime.UtcNow).ToString(_chequeNoFormat);
        }

        /// <summary>
        /// Gets the amount words.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetAmountWords(decimal amount)
        {
            decimal divider = 1;
            if (amount >= BILLION)
            {
                divider = BILLION;
            }
            else if (amount >= MILLION)
            {
                divider = MILLION;
            }
            else if (amount >= THOUSANDS)
            {
                divider = THOUSANDS;
            }
            else if (amount >= HUNDREDS)
            {
                divider = HUNDREDS;
            }

            if (divider >= HUNDREDS)
            {
                var leftAmount = (int)(amount / divider);
                var rightAmount = amount - (leftAmount * divider);
                var scale = leftAmount > 1 ? scales[divider] + "s" : scales[divider];
                return GetAmountWords(leftAmount) + " " + scale + 
                    (rightAmount == 0 ? "" : " " + GetAmountWords(rightAmount));
            }
            else if(amount >= 20)
            {
                var leftAmount = (int)(amount / 10);
                var rightAmount = amount - (leftAmount * 10);
                return textStrings[leftAmount * 10] + 
                    (rightAmount == 0 ? "" : " " + GetAmountWords(rightAmount));
            }
            else
            {
                return textStrings[amount];
            }
        }

        #endregion IChequeService

        #region Private Methods
        /// <summary>
        /// Validates the delete cheque.
        /// </summary>
        /// <param name="existingCheque">The existing cheque.</param>
        /// <returns></returns>
        private bool ValidateDeleteCheque<T>(Cheque existingCheque, ServiceResult<T> result)
        {
            // Can't delete printed cheque.
            if (existingCheque.Status == ChequeStatus.P.ToString())
            {
                result.ErrorMessages["Status"] = string.Format(MessagesRes.Cant__, CommonsRes.Delete,
                    string.Format(CommonsRes.Join2Words, CommonsRes.Printed, EntitiesRes.Cheque));
                return false;
            }
            // Can't delete canceled cheque.
            else if (existingCheque.Status == ChequeStatus.C.ToString())
            {
                result.ErrorMessages["Status"] = string.Format(MessagesRes.Cant__, CommonsRes.Delete,
                    string.Format(CommonsRes.Join2Words, CommonsRes.Canceled, EntitiesRes.Cheque));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the cancel cheque.
        /// </summary>
        /// <param name="existingCheque">The existing cheque.</param>
        /// <returns></returns>
        private bool ValidateCancelCheque<T>(Cheque existingCheque, ServiceResult<T> result)
        {
            // Only printed cheque that can be canceled.
            if (existingCheque.Status != ChequeStatus.P.ToString())
            {
                result.ErrorMessages["Status"] = string.Format(MessagesRes.Only_ThatCanBe_,
                    string.Format(CommonsRes.Join2Words, CommonsRes.Printed, EntitiesRes.Cheque),
                    CommonsRes.Canceled);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the print cheque.
        /// </summary>
        /// <param name="existingCheque">The existing cheque.</param>
        /// <returns></returns>
        private bool ValidatePrintCheque<T>(Cheque existingCheque, ServiceResult<T> result)
        {
            // Only active cheque that can be printed.
            if (existingCheque.Status != ChequeStatus.A.ToString())
            {
                result.ErrorMessages["Status"] = string.Format(MessagesRes.Only_ThatCanBe_,
                    string.Format(CommonsRes.Join2Words, CommonsRes.Active, EntitiesRes.Cheque),
                    CommonsRes.Printed);
                return false;
            }
            return true;
        }

        private void Initialize()
        {
            textStrings.Add(0, "zero");
            textStrings.Add(1, "one");
            textStrings.Add(2, "two");
            textStrings.Add(3, "three");
            textStrings.Add(4, "four");
            textStrings.Add(5, "five");
            textStrings.Add(6, "six");
            textStrings.Add(7, "seven");
            textStrings.Add(8, "eight");
            textStrings.Add(9, "nine");
            textStrings.Add(10, "ten");
            textStrings.Add(11, "eleven");
            textStrings.Add(12, "twelve");
            textStrings.Add(13, "thirteen");
            textStrings.Add(14, "fourteen");
            textStrings.Add(15, "fifteen");
            textStrings.Add(16, "sixteen");
            textStrings.Add(17, "seventeen");
            textStrings.Add(18, "eighteen");
            textStrings.Add(19, "nineteen");
            textStrings.Add(20, "twenty");
            textStrings.Add(30, "thirty");
            textStrings.Add(40, "forty");
            textStrings.Add(50, "fifty");
            textStrings.Add(60, "sixty");
            textStrings.Add(70, "seventy");
            textStrings.Add(80, "eighty");
            textStrings.Add(90, "ninety");

            scales.Add(BILLION, "billion");
            scales.Add(MILLION, "million");
            scales.Add(THOUSANDS, "thousand");
            scales.Add(HUNDREDS, "hundred");
        }

        #endregion Private Methods
    }
}
