using System.Web;

using Microsoft.Practices.Unity;
using Unity.WebForms;
using ChequeWriter.Commons;

[assembly: WebActivatorEx.PostApplicationStartMethod( typeof(ChequeWriter.Web.App_Start.UnityWebFormsStart), "PostStart" )]
namespace ChequeWriter.Web.App_Start
{
	/// <summary>
	///		Startup class for the Unity.WebForms NuGet package.
	/// </summary>
	internal static class UnityWebFormsStart
	{
		/// <summary>
		///     Initializes the unity container when the application starts up.
		/// </summary>
		/// <remarks>
		///		Do not edit this method. Perform any modifications in the
		///		<see cref="RegisterDependencies" /> method.
		/// </remarks>
		internal static void PostStart()
		{
			IUnityContainer container = new UnityContainer();
			HttpContext.Current.Application.SetContainer( container );

			RegisterDependencies( container );
		}

		/// <summary>
		///		Registers dependencies in the supplied container.
		/// </summary>
		/// <param name="container">Instance of the container to populate.</param>
		private static void RegisterDependencies( IUnityContainer container )
        {
            //container.RegisterType<IUserData, UserData>();
            //container.RegisterType<IUserDomain, UserDomain>();

            //Module initialization thru MEF
            ModuleLoader.LoadContainer(container, ".\\bin", "ChequeWriter.*.dll");
		}
	}
}