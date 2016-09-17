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
    public class ChequeRepository : EFBaseRepository<Cheque, long>, IChequeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChequeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ChequeRepository(ChequeWriterContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<Cheque> GetAll()
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
        public PagedResult<Cheque> Retrieve(int pageNumber, int pageSize, 
            IDictionary<string, string> searchCriteria = null,
            IList<string> orderCriteria = null)
        {
            IQueryable<Cheque> query = DbSet.Where(a => a.Status != ChequeStatus.R.ToString());
            if (searchCriteria != null && searchCriteria.Count > 0)
            {
                query = from cheque in query
                        where
                            searchCriteria.ContainsKey("Memo") ?
                                cheque.Memo.Contains(searchCriteria["Memo"]) : true ||
                            searchCriteria.ContainsKey("Status") ?
                                cheque.Status.Contains(searchCriteria["Status"]) : true
                        select cheque;
            }
            if (orderCriteria != null && orderCriteria.Count > 0)
            {
                var theType = typeof(Cheque);
                foreach (var order in orderCriteria)
                {
                    if (theType.GetProperty(order) != null)
                    {
                        query = query.OrderBy(order);
                    }
                }
            }
            var count = query.LongCount();
            var result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<Cheque>(result, count);
        }
    }
}
