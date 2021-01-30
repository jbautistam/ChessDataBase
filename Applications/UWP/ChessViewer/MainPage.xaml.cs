using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Bau.Libraries.LibChessGame.ViewModels;
using ChessViewer.Controllers;

namespace ChessViewer
{
    /// <summary>
    ///		Página principal de la aplicación
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
			HostController = new HostController(AppDomain.CurrentDomain.BaseDirectory);
			DataContext = ViewModel = new MainViewModel(HostController);
        }

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private async Task InitFormAsync()
		{
			// Inicializa el viewModel principal
			await ViewModel.InitAsync();
			// Carga los datos de configuración
			Configuration.Load();
			// Inicia los directorios de imágenes
			if (!string.IsNullOrEmpty(Configuration.PathBoardImages))
				ViewModel.ComboPathBoard.SelectedPath = Configuration.PathBoardImages;
			if (!string.IsNullOrEmpty(Configuration.PathPieceImages))
				ViewModel.ComboPathPieces.SelectedPath = Configuration.PathPieceImages;
			// Carga el archivo inicial
			if (!string.IsNullOrEmpty(Configuration.Game))
				await LoadGameAsync(Configuration.Game);
			// Asigna el resto de las propiedades
			ViewModel.MustShowAnimation = Configuration.MustShowAnimations;
			// Inicializa el tablero
			udtListMovements.ViewModel = ViewModel.GameBoardViewModel;
			udtMovementInfoView.Init(ViewModel.GameBoardViewModel);
			udtBoard.Init(ViewModel.GameBoardViewModel);
			// Asigna los manejadores de eventos (al final para que no se traten los eventos mientras se hace la carga)
			ViewModel.PgnLibraryViewModel.PropertyChanged += async (sender, args) =>
												{
													if (args.PropertyName.Equals(nameof(Bau.Libraries.LibChessGame.ViewModels.Pgn.PgnLibraryViewModel.FileName)))
													{
														// Carga el texto del archivo cuando se cambia de archivo
														await LoadTextFileAsync(ViewModel.PgnLibraryViewModel.FullFileName);
														// Graba la configuración
														SaveConfiguration();
													}
												};
			ViewModel.ComboPathBoard.PropertyChanged += (sender, args) =>
												{
													// Graba la configuración cuando se cambia la selección de imágenes
													if (args.PropertyName.Equals(nameof(PathComboImagesViewModel.SelectedPath)))
														SaveConfiguration();
												};
			ViewModel.ComboPathPieces.PropertyChanged += (sender, args) =>
												{
													// Graba la configuración cuando se cambia la selección de imágenes
													if (args.PropertyName.Equals(nameof(PathComboImagesViewModel.SelectedPath)))
														SaveConfiguration();
												};
		}

		/// <summary>
		///		Carga un juego
		/// </summary>
		private async Task LoadGameAsync(string fileName)
		{
			(bool loaded, string error) = await ViewModel.PgnLibraryViewModel.LoadAsync(fileName);

				// Graba la configuración
				if (loaded)
					SaveConfiguration();
				else
					await HostController.ShowMessageAsync(error);
		}

		/// <summary>
		///		Graba la configuración
		/// </summary>
		private void SaveConfiguration()
		{
			// Asigna los valores de las propiedades
			Configuration.PathBoardImages = ViewModel.ComboPathBoard.SelectedPath;
			Configuration.PathPieceImages = ViewModel.ComboPathPieces.SelectedPath;
			Configuration.MustShowAnimations = ViewModel.MustShowAnimation;
			if (!string.IsNullOrWhiteSpace(ViewModel?.PgnLibraryViewModel?.FullFileName))
				Configuration.Game = ViewModel.PgnLibraryViewModel.FullFileName;
			// Graba la configuración
			Configuration.Save();
		}

		/// <summary>
		///		Muestra / oculta el archivo
		/// </summary>
		private void ShowFile(bool showFile)
		{
			if (showFile)
			{
				udtBoard.Visibility = Visibility.Collapsed;
				scrTextFile.Visibility = txtFileContent.Visibility = Visibility.Visible;
			}
			else
			{
				udtBoard.Visibility = Visibility.Visible;
				scrTextFile.Visibility = txtFileContent.Visibility = Visibility.Collapsed;
			}
		}

		/// <summary>
		///		Carga el texto del archivo
		/// </summary>
		private async Task LoadTextFileAsync(string fileName)
		{
			// Vacía el texto del archivo
			txtFileContent.Text = string.Empty;
			// Lee el texto del archivo
			try
			{
				using (System.IO.StreamReader reader = await HostController.GetStreamReaderAsync(fileName))
				{
					txtFileContent.Text = await reader.ReadToEndAsync();
				}
			}
			catch (Exception exception)
			{
				await HostController.ShowMessageAsync($"Error when load file. {exception.Message}");
			}
		}

		/// <summary>
		///		ViewModel para el juego
		/// </summary>
		public MainViewModel ViewModel { get; }

		/// <summary>
		///		Controlador principal
		/// </summary>
		public HostController HostController { get; }

		/// <summary>
		///		Controlador de configuración
		/// </summary>
		public ConfigurationController Configuration { get; }	= new ConfigurationController();

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			await InitFormAsync();
		}

		private void cmdShowFile_Click(object sender, RoutedEventArgs e)
		{
			ShowFile(scrTextFile.Visibility == Visibility.Collapsed);
			cmdShowFile.IsChecked = scrTextFile.Visibility == Visibility.Visible;
		}
	}
}
