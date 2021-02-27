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
	public partial class ChessboardGameView : UserControl
	{
		public ChessboardGameView(GameBoardViewModel viewModel)
		{
			// Inicializa el viewModel
			DataContext = ViewModel = viewModel;
			// Inicializa los componentes
			InitializeComponent();
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private void InitForm()
		{
			// Inicializa el tablero
			udtListMovements.ViewModel = ViewModel.ChessGameViewModel.GameBoardViewModel;
			udtMovementInfoView.Init(ViewModel.ChessGameViewModel.GameBoardViewModel);
			udtBoard.Init(ViewModel.ChessGameViewModel.GameBoardViewModel);
			// Inicializa el juego
			ViewModel.ChessGameViewModel.Init();
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		public GameBoardViewModel ViewModel { get; }

		private void lstMovements_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListBox lstView && ViewModel.ChessGameViewModel.GameBoardViewModel?.MovementsList?.SelectedMovement != null)
				lstView.ScrollIntoView(ViewModel.ChessGameViewModel.GameBoardViewModel.MovementsList.SelectedMovement);
		}

		private void UserControl_Initialized(object sender, EventArgs e)
		{
			InitForm();
		}
	}
}
