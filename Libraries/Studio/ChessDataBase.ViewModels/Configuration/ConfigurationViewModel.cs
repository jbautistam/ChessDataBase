using System;

namespace Bau.Libraries.ChessDataBase.ViewModels.Configuration
{
	/// <summary>
	///		ViewModel de la configuración
	/// </summary>
	public class ConfigurationViewModel : BauMvvm.ViewModels.Forms.Dialogs.BaseDialogViewModel
	{
		// Variables privadas
		private string _pathBoardImages, _pathPiecesImages;
		private bool _showAnimations;
		private Tools.LastFilesListViewModel _lastFilesViewModel;
		private string _workspace;

		public ConfigurationViewModel(MainViewModel mainViewModel)
		{
			// Guarda las propidades
			MainViewModel = mainViewModel;
			// Inicializa los objetos
			LastFilesViewModel = new Tools.LastFilesListViewModel(this);
		}

		/// <summary>
		///		Carga la configuración
		/// </summary>
		internal void Load()
		{
			MainViewModel.MainController.LoadConfiguration(this);
		}

		/// <summary>
		///		Comprueba los datos introducidos en el formulario
		/// </summary>
		private bool ValidateData()
		{
			return true;
		}

		/// <summary>
		///		Graba la configuración
		/// </summary>
		protected override void Save()
		{
			if (ValidateData())
			{
				// Graba la configuración
				MainViewModel.MainController.SaveConfiguration(this);
				// Cierra la ventana
				RaiseEventClose(true);
			}
		}

		/// <summary>
		///		ViewModel principal
		/// </summary>
		public MainViewModel MainViewModel { get; }

		/// <summary>
		///		Directorio donde se encuentran las imágenes del tablero
		/// </summary>
		public string PathBoardImages 
		{ 
			get { return _pathBoardImages; }
			set { CheckProperty(ref _pathBoardImages, value); }
		}

		/// <summary>
		///		Directorio donde se encuentran las imágenes de las piezas
		/// </summary>
		public string PathPiecesImages
		{ 
			get { return _pathPiecesImages; }
			set { CheckProperty(ref _pathPiecesImages, value); }
		}

		/// <summary>
		///		Indica si se deben mostrar animaciones
		/// </summary>
		public bool ShowAnimations
		{ 
			get { return _showAnimations; }
			set { CheckProperty(ref _showAnimations, value); }
		}

		/// <summary>
		///		Ultimo directorio seleccionado al abrir / grabar un archivo
		/// </summary>
		public string LastPathSelected { get; set; }

		/// <summary>
		///		ViewModel de los últimos archivos abiertos
		/// </summary>
		public Tools.LastFilesListViewModel LastFilesViewModel
		{
			get { return _lastFilesViewModel; }
			set { CheckObject(ref _lastFilesViewModel, value); }
		}

		/// <summary>
		///		Espacio de trabajo
		/// </summary>
		public string Workspace
		{
			get { return _workspace; }
			set { CheckProperty(ref _workspace, value); }
		}
	}
}
