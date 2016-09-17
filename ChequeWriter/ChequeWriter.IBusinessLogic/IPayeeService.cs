using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.IBusinessLogic
{
    /// <summary>
    /// Interface for Payee Service
    /// </summary>
    /// <seealso cref="ChequeWriter.IBusinessLogic.IService{ChequeWriter.DTO.Models.Payee,System.Int64}" />
    public interface IPayeeService : IService<Payee, long>
    {
    }
}
