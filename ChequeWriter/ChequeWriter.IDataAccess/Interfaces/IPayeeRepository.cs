using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.IDataAccess.Interfaces
{
    public interface IPayeeRepository : IRepository<Payee, long>
    {
    }
}
