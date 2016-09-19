using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.IBusinessLogic
{
    /// <summary>
    /// Interface for Cheque Service
    /// </summary>
    /// <seealso cref="ChequeWriter.IBusinessLogic.IService{ChequeWriter.DTO.Models.Cheque,System.Int64}" />
    public interface IChequeService : IService<Cheque, long>
    {
        IServiceResult<bool> CancelCheque(long chequeId);

        IServiceResult<bool> PrintCheque(long chequeId);

        string GenerateChequeNumber(DateTime? datetime = null);

        string GetAmountWords(decimal amount);
    }
}
