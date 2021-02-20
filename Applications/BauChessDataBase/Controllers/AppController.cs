﻿using System;

namespace Bau.ChessDataBase.Controllers
{
	/// <summary>
	///		Controlador principal de la aplicación
	/// </summary>
	public class AppController
	{
		public AppController(string applicationName, MainWindow mainWindow, string appPath)
		{
			SparkSolutionController = new DbStudioController(this, applicationName, mainWindow, appPath);
		}

		/// <summary>
		///		Controlador de la solución
		/// </summary>
		public DbStudioController SparkSolutionController { get; }

		/// <summary>
		///		Controlador de configuración de la aplicación
		/// </summary>
		public AppConfigurationController ConfigurationController { get; } = new AppConfigurationController();
	}
}
