using ChequeWriter.IDataAccess.Interfaces;
using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChequeWriter.Commons;

namespace ChequeWriter.DataAccess.EF.Repository
{
    public class PayeeRepository : EFBaseRepository<Payee, long>, IPayeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayeeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PayeeRepository(ChequeWriterContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<Payee> GetAll()
        {
            return base.Get().ToList();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <returns></returns>
        public long GetCount()
        {
            return base.DbSet.LongCount();
        }

        /// <summary>
        /// Retrieves the specified page number.
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
            IQueryable<Payee> query = DbSet.Where(a => a.Status != PayeeStatus.R.ToString());
            if (searchCriteria != null && searchCriteria.Count > 0)
            {
                query = from payee in query
                        where
                            searchCriteria.ContainsKey("FirtsName") ?
                                payee.FirstName.Contains(searchCriteria["FirstName"]) : true ||
                            searchCriteria.ContainsKey("LastName") ?
                                payee.LastName.Contains(searchCriteria["LastName"]) : true
                        select payee;
            }
            if (orderCriteria != null && orderCriteria.Count > 0)
            {
                var theType = typeof(Payee);
                foreach (var order in orderCriteria)
                {
                    if (theType.GetProperty(order) != null)
                    {
                        query = query.OrderBy(order);
                    }
                }
            }
            else
            {
                query = query.OrderBy(a => a.FirstName);
            }
            var count = query.LongCount();
            var result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<Payee>(result, count);
        }
    }
}
