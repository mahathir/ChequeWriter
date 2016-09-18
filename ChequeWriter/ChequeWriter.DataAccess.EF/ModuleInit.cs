using ChequeWriter.Commons;
using ChequeWriter.IDataAccess;
using System.ComponentModel.Composition;

namespace ChequeWriter.DataAccess.EF
{
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterType<IUnitOfWorks, UnitOfWorks>();
        }
    }
}
