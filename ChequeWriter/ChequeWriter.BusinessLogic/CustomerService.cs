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
    /// Customer Service
    /// </summary>
    /// <seealso cref="ChequeWriter.BusinessLogic.BaseService" />
    /// <seealso cref="ChequeWriter.IBusinessLogic.ICustomerService" />
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IUnitOfWorks uof)
            : base(uof)
        {
        }

        /// <summary>
        /// Creates the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public IServiceResult<Customer> Create(Customer customer)
        {
            var result = new ServiceResult<Customer>();

            var existingCustomer = UnitOfWork.CustomerRepo.Retrieve(customer.CustomerID);

            if (existingCustomer != null)
            {
                result.ErrorMessages.Add("CustomerID", string.Format(MessagesRes._Already_, EntitiesRes.Customer,
                    CommonsRes.Exists));
                return result;
            }

            var pagedResult = UnitOfWork.CustomerRepo.Retrieve(1, 1,
                new Dictionary<string, string> { { "CustomerNo", customer.CustomerNo } });

            if (pagedResult.TotalCount > 0)
            {
                result.ErrorMessages.Add("CustomerNo", string.Format(MessagesRes._Already_, EntitiesRes.CustomerNo,
                    CommonsRes.Exists));
                return result;
            }

            customer.Status = CustomerStatus.A.ToString();
            UnitOfWork.CustomerRepo.Create(customer);
            UnitOfWork.SaveChanges();

            result.Result = customer;
            return result;
        }

        /// <summary>
        /// Retrieves the specified customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Customer</returns>
        public Customer Retrieve(long id)
        {
            return UnitOfWork.CustomerRepo.Retrieve(id);
        }

        /// <summary>
        /// Retrieves the customer list.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="orderCriteria">The order criteria.</param>
        /// <returns></returns>
        public PagedResult<Customer> Retrieve(int pageNumber, int pageSize, 
            IDictionary<string, string> searchCriteria = null, 
            IList<string> orderCriteria = null)
        {
            return UnitOfWork.CustomerRepo.Retrieve(pageNumber, pageSize, searchCriteria, orderCriteria);
        }

        /// <summary>
        /// Updates the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public IServiceResult<bool> Update(Customer customer)
        {
            var result = new ServiceResult<bool>();
            var existingCustomer = UnitOfWork.CustomerRepo.Retrieve(customer.CustomerID);

            if (existingCustomer == null)
            {
                result.ErrorMessages.Add("CustomerID", string.Format(MessagesRes._NotFound, EntitiesRes.Customer));
                return result;
            }

            UnitOfWork.CustomerRepo.Update(customer);
            UnitOfWork.SaveChanges();
            return result;
        }

        /// <summary>
        /// Deletes the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public IServiceResult<bool> Delete(Customer customer)
        {
            return Delete(customer.CustomerID);
        }

        /// <summary>
        /// Deletes the specified customer.
        /// </summary>
        /// <param name="id">The customer id.</param>
        public IServiceResult<bool> Delete(long id)
        {
            var result = new ServiceResult<bool>();
            var cust = UnitOfWork.CustomerRepo.Retrieve(id);
            if (cust != null)
            {
                cust.Status = CustomerStatus.R.ToString();
                UnitOfWork.CustomerRepo.Update(cust);
                UnitOfWork.SaveChanges();
                result.Result = true;
            }
            return result;
        }
    }
}
