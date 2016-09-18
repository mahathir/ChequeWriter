using Microsoft.Practices.Unity;
using ChequeWriter.Commons;
using System.ComponentModel.Composition;
using ChequeWriter.IBusinessLogic;

namespace ChequeWriter.BusinessLogic
{
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterType<ICustomerService, CustomerService>();
            registrar.RegisterType<IChequeService, ChequeService>();
            registrar.RegisterType<IPayeeService, PayeeService>();
        }
    }
}
