using ChequeWriter.IDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.IDataAccess
{
    public interface IUnitOfWorks : IDisposable
    {
        IChequeRepository ChequeRepo { get; }
        ICustomerRepository CustomerRepo { get; }
        IPayeeRepository PayeeRepo { get; }
        void SaveChanges();
    }
}
