using ChequeWriter.DataAccess.EF.Repository;
using ChequeWriter.IDataAccess;
using ChequeWriter.IDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.DataAccess.EF
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private ChequeWriterContext _context = new ChequeWriterContext();
        private IChequeRepository _chequeRepo;
        private ICustomerRepository _customerRepo;
        private IPayeeRepository _payeeRepo;
        private bool _disposed = false;

        public IChequeRepository ChequeRepo
        {
            get
            {

                if (_chequeRepo == null)
                {
                    _chequeRepo = new ChequeRepository(_context);
                }
                return _chequeRepo;
            }
        }

        public ICustomerRepository CustomerRepo
        {
            get
            {

                if (_customerRepo == null)
                {
                    _customerRepo = new CustomerRepository(_context);
                }
                return _customerRepo;
            }
        }

        public IPayeeRepository PayeeRepo
        {
            get
            {

                if (_payeeRepo == null)
                {
                    _payeeRepo = new PayeeRepository(_context);
                }
                return _payeeRepo;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
