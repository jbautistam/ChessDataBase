using System;

using Bau.Libraries.BauMvvm.ViewModels;
using Bau.Libraries.ChessDataBase.Application;

namespace Bau.Libraries.ChessDataBase.ViewModels
{
	/// <summary>
	///		ViewModel principal
	/// </summary>
	public class MainViewModel : BaseObservableObject
	{
		// Constantes privadas
		internal const string MaskFiles = "Archivos de solución (*.dbsln)|*.dbsln|Todos los archivos (*.*)|*.*";
		// Eventos públicos
		public event EventHandler WorkspacesChanged;
		// Variables privadas
		private string _text;
		private Interfaces.IDetailViewModel _selectedDetailsViewModel;
		private Tools.LogListViewModel _logViewModel;
		private Tools.Search.SearchFilesViewModel _searchFilesViewModel;
		private Configuration.ConfigurationViewModel _configurationViewModel;

		public MainViewModel(Controllers.IChessDataBaseStudioController mainController)
		{
			// Título de la aplicación
			Text = mainController.AppName;
			// Asigna las propiedades
			Instance = this;
			MainController = mainController;
			Manager = new SolutionManager(mainController.Logger, mainController.AppPath);
			// Inicializa los objetos
			SolutionViewModel = new Solutions.SolutionViewModel(this);
			// Inicializa los objetos adicionales
			LogViewModel = new Tools.LogListViewModel(this);
			SearchFilesViewModel = new Tools.Search.SearchFilesViewModel(this);
			ConfigurationViewModel = new Configuration.ConfigurationViewModel(this);
			// Asigna los comandos
			SaveCommand = new BaseCommand(_ => Save(false), _ => CanSave())
									.AddListener(this, nameof(SelectedDetailsViewModel));
			SaveAsCommand = new BaseCommand(_ => Save(true), _ => CanSave())
									.AddListener(this, nameof(SelectedDetailsViewModel));
			SaveAllCommand = new BaseCommand(_ => SaveAll(), _ => CanSave())
									.AddListener(this, nameof(SelectedDetailsViewModel));
			RefreshCommand = new BaseCommand(_ => Refresh());
			OpenConfigurationCommand = new BaseCommand(_ => OpenConfigurationView());
			NewGameCommand = new BaseCommand(_ => OpenNewGameView());
		}

		/// <summary>
		///		Inicializa el viewModel
		/// </summary>
		public void Init()
		{
			ConfigurationViewModel.Load();
			SolutionViewModel.Load();
		}

		/// <summary>
		///		Graba la solución
		/// </summary>
		internal void SaveSolution()
		{
			Manager.SaveSolution(SolutionViewModel.Solution);
		}

		/// <summary>
		///		Actualiza el árbol
		/// </summary>
		internal void Refresh()
		{
			SolutionViewModel.Load();
		}

		/// <summary>
		///		Comprueba si puede guardar el contenido de la ventana
		/// </summary>
		private bool CanSave()
		{
			return SelectedDetailsViewModel != null;
		}

		/// <summary>
		///		Graba el viewModel activo
		/// </summary>
		private void Save(bool newName)
		{
			if (SelectedDetailsViewModel != null)
				SelectedDetailsViewModel.SaveDetails(newName);
		}

		/// <summary>
		///		Graba todos las ventanas de edición abiertas
		/// </summary>
		private void SaveAll()
		{
			foreach (Interfaces.IDetailViewModel viewModel in MainController.GetOpenedDetails())
				if (viewModel.IsUpdated)
					viewModel.SaveDetails(false);
		}

		/// <summary>
		///		Solicita al usuario un nombre de archivos. Guarda el directorio seleccionado para que la próxima vez que se consulte
		///	por un archivo, se vaya directamente a ese directorio
		/// </summary>
		internal string OpenDialogSave(string suggestedFileName, string mask, string defaultExtension)
		{
			string path = GetPath(suggestedFileName);
			string fileName = MainController.HostController.DialogsController.OpenDialogSave(path, mask, suggestedFileName, defaultExtension);

				// Si se ha escogido un nombre de archivo se guarda el último directorio seleccionado
				if (!string.IsNullOrWhiteSpace(fileName) && System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fileName)))
					ConfigurationViewModel.LastPathSelected = System.IO.Path.GetDirectoryName(fileName);
				// Devuelve el nombre de archivo
				return fileName;
		}

		/// <summary>
		///		Obtiene el directorio inicial de grabación de un archivo
		/// </summary>
		private string GetPath(string suggestedFileName)
		{
			string path = string.Empty;

				// Obtiene el directorio
				if (!string.IsNullOrWhiteSpace(suggestedFileName))
				{
					path = System.IO.Path.GetDirectoryName(suggestedFileName);
					if (string.IsNullOrWhiteSpace(path) || path.Equals(suggestedFileName, StringComparison.CurrentCultureIgnoreCase))
						path = ConfigurationViewModel.LastPathSelected;
				}
				// Si no se ha definido ningún directorio, se coge el de la solución
				if (string.IsNullOrWhiteSpace(path))
					path = SolutionViewModel.Solution.Path;
				// Devuelve el directorio
				return path;
		}

		/// <summary>
		///		Abre la ventana de configuración
		/// </summary>
		private void OpenConfigurationView()
		{
			MainController.OpenDialog(ConfigurationViewModel);
		}

		private void OpenNewGameView()
		{
			MainController.OpenWindow(new Solutions.Details.Games.GameBoardViewModel(SolutionViewModel));
		}

		/// <summary>
		///		Lanza el evento de modificación de los workspaces
		/// </summary>
		internal void RaiseEventWorkSpaceChanged()
		{
			WorkspacesChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///		Instancia principal 
		/// </summary>
		public MainViewModel Instance { get; }

		/// <summary>
		///		Controlador principal
		/// </summary>
		public Controllers.IChessDataBaseStudioController MainController { get; }

		/// <summary>
		///		Manager de solución
		/// </summary>
		internal SolutionManager Manager { get; }

		/// <summary>
		///		ViewModel de la solución
		/// </summary>
		public Solutions.SolutionViewModel SolutionViewModel { get; }

		/// <summary>
		///		ViewModel de detalles seleccionado en la ventana principal
		/// </summary>
		public Interfaces.IDetailViewModel SelectedDetailsViewModel
		{
			get { return _selectedDetailsViewModel; }
			set { CheckObject(ref _selectedDetailsViewModel, value); }
		}

		/// <summary>
		///		Título de la ventana
		/// </summary>
		public string Text 
		{
			get { return _text; }
			set { CheckProperty(ref _text, value); }
		}

		/// <summary>
		///		ViewModel de la configuración
		/// </summary>
		public Configuration.ConfigurationViewModel ConfigurationViewModel
		{
			get { return _configurationViewModel; }
			set { CheckObject(ref _configurationViewModel, value); }
		}

		/// <summary>
		///		ViewModel de log
		/// </summary>
		public Tools.LogListViewModel LogViewModel
		{
			get { return _logViewModel; }
			set { CheckObject(ref _logViewModel, value); }
		}

		/// <summary>
		///		ViewModel de búsqueda de archivos
		/// </summary>
		public Tools.Search.SearchFilesViewModel SearchFilesViewModel
		{
			get { return _searchFilesViewModel; }
			set { CheckObject(ref _searchFilesViewModel, value); }
		}

		/// <summary>
		///		Comando para grabar el elemento actual
		/// </summary>
		public BaseCommand SaveCommand { get; }

		/// <summary>
		///		Comando para grabar el elemento actual con un nuevo nombre
		/// </summary>
		public BaseCommand SaveAsCommand { get; }

		/// <summary>
		///		Comando para grabar todos los elementos abiertos
		/// </summary>
		public BaseCommand SaveAllCommand { get; }

		/// <summary>
		///		Comando para actualizar los datos
		/// </summary>
		public BaseCommand RefreshCommand { get; }

		/// <summary>
		///		Comando para abrir la ventana de configuración
		/// </summary>
		public BaseCommand OpenConfigurationCommand { get; }

		/// <summary>
		///		Comando para abrir un juego nuevo
		/// </summary>
		public BaseCommand NewGameCommand { get; }
	}
}
