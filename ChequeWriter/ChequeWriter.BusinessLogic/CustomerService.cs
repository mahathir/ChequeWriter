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
        public void Create(Customer customer)
        {
            customer.Status = CustomerStatus.A.ToString();
            UnitOfWork.CustomerRepo.Create(customer);
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
        public void Update(Customer customer)
        {
            UnitOfWork.CustomerRepo.Update(customer);
        }

        /// <summary>
        /// Deletes the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void Delete(Customer customer)
        {
            var cust = UnitOfWork.CustomerRepo.Retrieve(customer.CustomerID);
            if (cust != null)
            {
                cust.Status = CustomerStatus.R.ToString();
                UnitOfWork.CustomerRepo.Update(cust);
            }
        }

        /// <summary>
        /// Deletes the specified customer.
        /// </summary>
        /// <param name="id">The customer id.</param>
        public void Delete(long id)
        {
            var cust = UnitOfWork.CustomerRepo.Retrieve(id);
            if (cust != null)
            {
                cust.Status = CustomerStatus.R.ToString();
                UnitOfWork.CustomerRepo.Update(cust);
            }
        }
    }
}
