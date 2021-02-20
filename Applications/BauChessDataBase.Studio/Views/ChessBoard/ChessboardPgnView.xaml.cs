using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

using Bau.Libraries.ChessDataBase.ViewModels.Solutions.Details.Games;

namespace Bau.ChessDataBase.Studio.Views.ChessBoard
{
	/// <summary>
	///		Control de usuario para mostrar el tablero con un archivo PGN
	/// </summary>
	public partial class ChessboardPgnView : UserControl
	{
		public ChessboardPgnView(GameBoardPgnViewModel viewModel)
		{
			// Inicializa los componentes
			InitializeComponent();
			// Inicializa el viewModel
			DataContext = ViewModel = viewModel;
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private async Task InitFormAsync()
		{
			// Inicializa el tablero
			udtListMovements.ViewModel = ViewModel.ChessGameViewModel.GameBoardViewModel;
			udtMovementInfoView.Init(ViewModel.ChessGameViewModel.GameBoardViewModel);
			udtBoard.Init(ViewModel.ChessGameViewModel.GameBoardViewModel);
			//// Carga el archivo inicial
			//if (!string.IsNullOrEmpty(Properties.Settings.Default.Game))
			//	await LoadGameAsync(Properties.Settings.Default.Game);
			//// Inicia los directorios de imágenes
			//if (!string.IsNullOrEmpty(Properties.Settings.Default.PathBoardImages))
			//	ViewModel.ComboPathBoard.SelectedPath = Properties.Settings.Default.PathBoardImages;
			//if (!string.IsNullOrEmpty(Properties.Settings.Default.PathPieceImages))
			//	ViewModel.ComboPathPieces.SelectedPath = Properties.Settings.Default.PathPieceImages;
			//// Asigna el resto de las propiedades
			//ViewModel.MustShowAnimation = Properties.Settings.Default.MustShowAnimations;
			// Carga el juego predeterminado
			await LoadGameAsync();
		}

		/// <summary>
		///		Carga el juego
		/// </summary>
		private async Task LoadGameAsync()
		{
			(bool isLoaded, string error) = await ViewModel.ChessGameViewModel.LoadFileAsync();

				if (!isLoaded)
					MessageBox.Show(error);
		}

		/// <summary>
		///		Muestra el archivo
		/// </summary>
		private void ShowFile()
		{
			//if (string.IsNullOrWhiteSpace(ViewModel.PgnLibraryViewModel.FullFileName))
			//	MessageBox.Show("Select a file");
			//else
			//{
			//	Views.ShowFilePgnView fileView = new Views.ShowFilePgnView();

			//		// Carga el archivo
			//		fileView.ShowFile(ViewModel.PgnLibraryViewModel.FullFileName);
			//		// Muestra la ventana
			//		fileView.Owner = this;
			//		fileView.Show();
			//}
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		public GameBoardPgnViewModel ViewModel { get; }

		private void lstMovements_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListBox lstView && ViewModel.ChessGameViewModel.GameBoardViewModel?.MovementsList?.SelectedMovement != null)
				lstView.ScrollIntoView(ViewModel.ChessGameViewModel.GameBoardViewModel.MovementsList.SelectedMovement);
		}

		private void cmdShowFile_Click(object sender, RoutedEventArgs e)
		{
			ShowFile();
		}

		private async void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			await InitFormAsync();
		}
	}
}
