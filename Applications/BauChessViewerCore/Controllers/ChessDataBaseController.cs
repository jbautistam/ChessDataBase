using System;
using System.Collections.Generic;
using System.IO;

using Bau.Libraries.LibLogger.Core;
using Bau.Libraries.BauMvvm.ViewModels.Controllers;
using Bau.Libraries.BauMvvm.ViewModels.Forms.Dialogs;
using Bau.Libraries.LibChessGame.ViewModels.Controllers;

namespace Bau.ChessDataBase.Controllers
{
	/// <summary>
	///		Controlador principal
	/// </summary>
	public class ChessDataBaseController : IChessGameController
	{
		// Constantes privadas
		private const string SubPathBoard = @"Data\Graphics\Boards";
		private const string SubPathPiecess = @"Data\Graphics\Pieces";

		public ChessDataBaseController(string appName, string pathBase, System.Windows.Window mainWindow)
		{
			AppName = appName;
			AppPath = pathBase;
			HostController = new Libraries.BauMvvm.Views.Wpf.Controllers.HostController(appName, mainWindow);
		}

		public SystemControllerEnums.ResultType OpenWindow(IDetailViewModel detailsViewModel)
		{
			throw new NotImplementedException();
		}

		public SystemControllerEnums.ResultType OpenDialog(BaseDialogViewModel dialogViewModel)
		{
			throw new NotImplementedException();
		}

		public void OpenExplorer(string path)
		{
			throw new NotImplementedException();
		}

		public IDetailViewModel GetActiveDetails()
		{
			throw new NotImplementedException();
		}

		public List<IDetailViewModel> GetOpenedDetails()
		{
			throw new NotImplementedException();
		}

		public void UpdateTabId(string oldTabId, string newTabId, string newHeader)
		{
			throw new NotImplementedException();
		}

		public void CloseWindow(string tabId)
		{
			throw new NotImplementedException();
		}

		public void ShowNotification(SystemControllerEnums.NotificationType type, string title, string message)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///		Obtiene los directorios donde se encuentran las imágenes del tablero
		/// </summary>
		public List<string> GetPathsBoard()
		{
			return GetSubPaths(Path.Combine(AppPath, SubPathBoard));
		}

		/// <summary>
		///		Obtiene los directorios donde se encuentran las imágenes de las piezas
		/// </summary>
		public List<string> GetPathsPieces()
		{
			return GetSubPaths(Path.Combine(AppPath, SubPathPiecess));
		}

		/// <summary>
		///		Obtiene los subdirectorios de un directorio
		/// </summary>
		private List<string> GetSubPaths(string path)
		{
			List<string> directories = new List<string>();

				// Obtiene los directorios hijo
				if (Directory.Exists(path))
					foreach (string directory in Directory.GetDirectories(path))
						directories.Add(directory);
				// Devuelve la lista de directorios
				return directories;
		}

		/// <summary>
		///		Nombre de la aplicación
		/// </summary>
		public string AppName { get; }

		/// <summary>
		///		Directorio base de la aplicación
		/// </summary>
		public string AppPath { get; }

		/// <summary>
		///		Log de la aplicación
		/// </summary>
		public LogManager Logger { get; }

		/// <summary>
		///		Controlador principal de la aplicación
		/// </summary>
		public IHostController HostController { get; }
	}
}
