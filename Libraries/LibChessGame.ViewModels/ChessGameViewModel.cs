using System;
using System.Threading.Tasks;

namespace Bau.Libraries.LibChessGame.ViewModels
{
	/// <summary>
	///		ViewModel de la ventana principal
	/// </summary>
	public class ChessGameViewModel : ChessGameBaseViewModel
	{
		public ChessGameViewModel(string pathBoardImages, string pathPiecesImages) : base(pathBoardImages, pathPiecesImages)
		{
		}

		/// <summary>
		///		Inicializa el tablero
		/// </summary>
		/// <returns></returns>
		public void Init()
		{
			GameBoardViewModel.LoadMovements(null);
		}
	}
}
