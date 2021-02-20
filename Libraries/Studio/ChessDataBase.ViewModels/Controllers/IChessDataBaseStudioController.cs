﻿using System;
using System.Collections.Generic;

using Bau.Libraries.BauMvvm.ViewModels.Controllers;
using Bau.Libraries.ChessDataBase.ViewModels.Interfaces;

namespace Bau.Libraries.ChessDataBase.ViewModels.Controllers
{
	/// <summary>
	///		Interface para los controladores de solución
	/// </summary>
	public interface IChessDataBaseStudioController
	{
		/// <summary>
		///		Controlador principal
		/// </summary>
		IHostController HostController { get; }

		/// <summary>
		///		Controlador de log
		/// </summary>
		LibLogger.Core.LogManager Logger { get; }

		/// <summary>
		///		Nombre de la aplicación
		/// </summary>
		string AppName { get; }

		/// <summary>
		///		Directorio de aplicación
		/// </summary>
		string AppPath { get; }

		/// <summary>
		///		Abre una ventana de detalles
		/// </summary>
		SystemControllerEnums.ResultType OpenWindow(IDetailViewModel detailsViewModel);

		/// <summary>
		///		Abre un cuadro de diálogo
		/// </summary>
		SystemControllerEnums.ResultType OpenDialog(BauMvvm.ViewModels.Forms.Dialogs.BaseDialogViewModel dialogViewModel);

		/// <summary>
		///		Abre el explorador sobre un directorio
		/// </summary>
		void OpenExplorer(string path);

		/// <summary>
		///		Obtiene la ventana de detalles activa
		/// </summary>
		IDetailViewModel GetActiveDetails();

		/// <summary>
		///		Obtiene la lista de ventanas de detalles abiertas
		/// </summary>
		List<IDetailViewModel> GetOpenedDetails();

		/// <summary>
		///		Indica a la ventana principal que cambie el Id de un documento
		/// </summary>
		void UpdateTabId(string oldTabId, string newTabId, string newHeader);

		/// <summary>
		///		Indica a la ventana principal que cierre un documento
		/// </summary>
		void CloseWindow(string tabId);

		/// <summary>
		///		Muestra una notificación (sólo si la configuración lo permite
		/// </summary>
		void ShowNotification(SystemControllerEnums.NotificationType type, string title, string message);

		/// <summary>
		///		Comprueba si en el portapapeles hay una imagen
		/// </summary>
		bool ClipboardContainImage();

		/// <summary>
		///		Graba la imagen del portapapeles
		/// </summary>
		bool SaveClipboardImage(string fileName);

		/// <summary>
		///		Carga la configuración
		/// </summary>
		void LoadConfiguration(Configuration.ConfigurationViewModel viewModel);

		/// <summary>
		///		Graba la configuración
		/// </summary>
		void SaveConfiguration(Configuration.ConfigurationViewModel viewModel);
	}
}
