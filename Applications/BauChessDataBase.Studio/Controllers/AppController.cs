using System;

namespace Bau.ChessDataBase.Studio.Controllers
{
	/// <summary>
	///		Controlador principal de la aplicación
	/// </summary>
	public class AppController
	{
		public AppController(string applicationName, MainWindow mainWindow, string appPath)
		{
			ChessDataBaseController = new ChessDataBaseStudioController(this, applicationName, mainWindow, appPath);
		}

		/// <summary>
		///		Controlador de la solución
		/// </summary>
		public ChessDataBaseStudioController ChessDataBaseController { get; }

		/// <summary>
		///		Controlador de configuración de la aplicación
		/// </summary>
		public AppConfigurationController ConfigurationController { get; } = new AppConfigurationController();
	}
}
