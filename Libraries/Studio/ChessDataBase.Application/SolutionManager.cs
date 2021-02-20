using System;

using Bau.Libraries.LibLogger.Core;
using Bau.Libraries.ChessDataBase.Models;

namespace Bau.Libraries.ChessDataBase.Application
{
	/// <summary>
	///		Manager de soluciones
	/// </summary>
	public class SolutionManager
	{
		// Constantes privadas
		private const string DefaultWorkspace = "ChessDataBase.Studio.Configuration";

		public SolutionManager(LogManager logger, string pathConfiguration)
		{
			Logger = logger;
			PathConfiguration = pathConfiguration;
		}

		/// <summary>
		///		Carga los datos de configuración
		/// </summary>
		public SolutionModel LoadConfiguration(string workspace)
		{
			WorkSpace = workspace;
			return new Repository.SolutionRepository().Load(GetConfigurationFileName());
		}

		/// <summary>
		///		Borra el archivo de configuración de un espacio de trabajo
		/// </summary>
		public void DeleteConfiguration(string workspace)
		{
			// Borra el archivo
			WorkSpace = workspace;
			LibHelper.Files.HelperFiles.KillFile(GetConfigurationFileName());
			// Carga el espacio de trabajo predeterminado
			LoadConfiguration(DefaultWorkspace);
		}

		/// <summary>
		///		Graba los datos de una solución
		/// </summary>
		public void SaveSolution(SolutionModel solution)
		{
			new Repository.SolutionRepository().Save(solution, GetConfigurationFileName());
		}

		/// <summary>
		///		Obtiene el nombre del archivo de configuración
		/// </summary>
		private string GetConfigurationFileName()
		{
			// Obtiene el directorio de configuración si no existía
			if (string.IsNullOrWhiteSpace(PathConfiguration))
				PathConfiguration = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			// Obtiene el directorio del espacio de trabajo
			if (string.IsNullOrWhiteSpace(WorkSpace))
				WorkSpace = DefaultWorkspace;
			// Devuelve el nombre del archivo de configuración
			return System.IO.Path.Combine(PathConfiguration, WorkSpace + ".xml");
		}

		/// <summary>
		///		Manager de log
		/// </summary>
		public LogManager Logger { get; }

		/// <summary>
		///		Directorio de configuración
		/// </summary>
		public string PathConfiguration { get; private set; }

		/// <summary>
		///		Nombre del archivo del espacio de trabajo
		/// </summary>
		public string WorkSpace { get; private set; }
	}
}