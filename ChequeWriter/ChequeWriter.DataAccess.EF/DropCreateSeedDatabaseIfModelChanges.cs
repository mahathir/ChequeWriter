using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ChequeWriter.DataAccess.EF
{
    public class DropCreateSeedDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<ChequeWriterContext>
    {
        protected override void Seed(ChequeWriterContext context)
        {
            // TODO: initialize data here.
        }
    }
}
