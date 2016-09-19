using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.Commons
{
    public enum CustomerStatus
    {
        INVALID = 0,
        [Description("Active")]
        A = 1, // Active
        [Description("Removed")]
        R = 2 // Removed
    }
    public enum PayeeStatus
    {
        INVALID = 0,
        [Description("Active")]
        A = 1, // Active
        [Description("Removed")]
        R = 2 // Removed
    }
    public enum ChequeStatus
    {
        INVALID = 0,
        [Description("Active")]
        A = 1, // Active
        [Description("Removed")]
        R = 2, // Removed
        [Description("Canceled")]
        C = 3, // Canceled
        [Description("Printed")]
        P = 4 // Printed
    }
}
