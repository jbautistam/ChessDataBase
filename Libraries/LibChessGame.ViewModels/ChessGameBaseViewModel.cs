using System;

namespace Bau.Libraries.LibChessGame.ViewModels
{
	/// <summary>
	///		Base para los viewModel de juegos de ajedrez
	/// </summary>
	public abstract class ChessGameBaseViewModel : BauMvvm.ViewModels.BaseObservableObject
	{
		// Variables privadas
		private Board.GameBoardViewModel _gameBoardViewModel;
		private string _pathBoardImages, _pathPiecesImages;

		public ChessGameBaseViewModel(string pathBoardImages, string pathPiecesImages) : base(false)
		{
			// Inicializa las propiedades
			PathBoardImages = pathBoardImages;
			PathPiecesImages = pathPiecesImages;
			// Inicializa los objetos
			GameBoardViewModel = new Board.GameBoardViewModel(this);
		}

		/// <summary>
		///		ViewModel del tablero de juego
		/// </summary>
		public Board.GameBoardViewModel GameBoardViewModel
		{
			get { return _gameBoardViewModel; }
			set { CheckProperty(ref _gameBoardViewModel, value); }
		}

		/// <summary>
		///		Directorio de imágenes del tablero
		/// </summary>
		public string PathBoardImages
		{	
			get { return _pathBoardImages; }
			set { CheckProperty(ref _pathBoardImages, value); }
		}

		/// <summary>
		///		Directorio de imágenes de las piezas
		/// </summary>
		public string PathPiecesImages
		{	
			get { return _pathPiecesImages; }
			set { CheckProperty(ref _pathPiecesImages, value); }
		}
	}
}
