using System;
using Windows.UI.Xaml.Controls;

using Bau.Libraries.LibChessGame.ViewModels.Board;

namespace ChessViewer.Views.Board
{
	/// <summary>
	///		Control que muestra el icono y el texto de un movimiento
	/// </summary>
	public sealed partial class MovementInfoView : UserControl
	{
		public MovementInfoView()
		{
			InitializeComponent();
		}

		/// <summary>
		///		Inicializa el ViewModel
		/// </summary>
		public void Init(GameBoardViewModel gameBoardViewModel)
		{
			DataContext = ViewModel = gameBoardViewModel;
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		public GameBoardViewModel ViewModel { get; private set; }
	}
}