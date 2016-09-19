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
            IDictionary<string, string> orderCriteria = null)
        {
            IQueryable<Cheque> query = DbSet.Where(a => a.Status != ChequeStatus.R.ToString());
            query = Search(searchCriteria, query);
            query = Sorting(query, a => a.PostingDate, orderCriteria);

            var count = query.LongCount();
            var result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<Cheque>(result, count);
        }
    }
}
