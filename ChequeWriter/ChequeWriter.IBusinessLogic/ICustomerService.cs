using ChequeWriter.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.IBusinessLogic
{
    /// <summary>
    /// Interface for Customer Service
    /// </summary>
    /// <seealso cref="ChequeWriter.IBusinessLogic.IService{ChequeWriter.DTO.Models.Customer,System.Int64}" />
    public interface ICustomerService : IService<Customer, long>
    {
        string GenerateNewCustomerNo();
    }
}
