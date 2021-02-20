using System;

using Bau.Libraries.ChessDataBase.ViewModels.Configuration;

namespace Bau.ChessDataBase.Studio.Controllers
{
	/// <summary>
	///		Controlador para la configuración de la aplicación
	/// </summary>
	public class AppConfigurationController
	{
		/// <summary>
		///		Carga la configuración
		/// </summary>
		public void Load(ConfigurationViewModel viewModel)
		{
			// Guarda el viewModel
			ViewModel = viewModel;
			// Asigna las propiedades de configuración
			viewModel.PathBoardImages = Properties.Settings.Default.PathBoardImages;
			viewModel.PathPiecesImages = Properties.Settings.Default.PathPiecesImages;
			viewModel.ShowAnimations = Properties.Settings.Default.ShowAnimations;
			viewModel.LastPathSelected = Properties.Settings.Default.LastPathSelected;
			viewModel.Workspace = Properties.Settings.Default.LastWorkSpace;
			viewModel.LastFilesViewModel.Add(Properties.Settings.Default.LastFiles);
			// Asigna las propiedades de la ventana
			LastThemeSelected = Properties.Settings.Default.LastThemeSelected;
			LastEncodingIndex = Properties.Settings.Default.LastEncodingIndex;
			EditorFontName = Properties.Settings.Default.EditorFontName;
			EditorFontSize = Properties.Settings.Default.EditorFontSize;
			EditorShowLinesNumber = Properties.Settings.Default.EditorShowLinesNumber;
			EditorZoom = Properties.Settings.Default.EditorZoom;
		}

		/// <summary>
		///		Graba la configuración
		/// </summary>
		public void Save(ConfigurationViewModel viewModel)
		{
			// Asigna las propiedades
			Properties.Settings.Default.PathBoardImages = viewModel.PathBoardImages;
			Properties.Settings.Default.PathPiecesImages = viewModel.PathPiecesImages;
			Properties.Settings.Default.ShowAnimations = viewModel.ShowAnimations;
			if (!string.IsNullOrWhiteSpace(viewModel.LastPathSelected))
				Properties.Settings.Default.LastPathSelected = viewModel.LastPathSelected;
			Properties.Settings.Default.LastWorkSpace = viewModel.Workspace;
			Properties.Settings.Default.LastFiles = viewModel.LastFilesViewModel.GetFiles();
			// Asigna las propiedades de las vistas
			Properties.Settings.Default.LastThemeSelected = LastThemeSelected;
			Properties.Settings.Default.LastEncodingIndex = LastEncodingIndex;
			Properties.Settings.Default.EditorFontName = EditorFontName;
			Properties.Settings.Default.EditorFontSize = EditorFontSize;
			Properties.Settings.Default.EditorShowLinesNumber = EditorShowLinesNumber;
			Properties.Settings.Default.EditorZoom = EditorZoom;
			// Graba la configuración
			Properties.Settings.Default.Save();
		}

		/// <summary>
		///		ViewModel de configuración
		/// </summary>
		public ConfigurationViewModel ViewModel { get; private set; }

		/// <summary>
		///		Ultimo tema seleccionado en la interface
		/// </summary>
		public int LastThemeSelected { get; set; }

		/// <summary>
		///		Indica de la codificación de archivo
		/// </summary>
		public int LastEncodingIndex { get; set; }

		/// <summary>
		///		Nombre de la fuente del editor
		/// </summary>
		public string EditorFontName { get; set; } = "Consolas";

		/// <summary>
		///		Tamaños de la fuente del editor
		/// </summary>
		public double EditorFontSize { get; set; } = 18;

		/// <summary>
		///		Indica si se debe mostrar el número de línea en el editor
		/// </summary>
		public bool EditorShowLinesNumber { get; set; } = true;

		/// <summary>
		///		Indica el nivel de zoom del editor
		/// </summary>
		public double EditorZoom { get; set; } = 1.0;
	}
}
