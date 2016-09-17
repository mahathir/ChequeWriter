using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.Commons
{
    public enum CustomerStatus
    {
        INVALID = 0,
        A = 1, // Active
        R = 2 // Removed
    }
    public enum PayeeStatus
    {
        INVALID = 0,
        A = 1, // Active
        R = 2 // Removed
    }
    public enum ChequeStatus
    {
        INVALID = 0,
        A = 1, // Active
        R = 2, // Removed
        C = 3, // Canceled
        P = 4 // Printed
    }
}
