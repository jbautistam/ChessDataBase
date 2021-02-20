using System;
using System.Threading.Tasks;

namespace Bau.Libraries.LibChessGame.ViewModels
{
	/// <summary>
	///		ViewModel de la ventana principal
	/// </summary>
	public class ChessGameViewModel : BauMvvm.ViewModels.BaseObservableObject
	{
		// Variables privadas
		private string _fileName;
		private Board.GameBoardViewModel _gameBoardViewModel;
		private Pgn.PgnLibraryViewModel _pgnLibraryViewModel;
		private string _pathBoardImages, _pathPiecesImages;

		public ChessGameViewModel(string fileName, string pathBoardImages, string pathPiecesImages) : base(false)
		{
			// Inicializa las propiedades
			FileName = fileName;
			PathBoardImages = pathBoardImages;
			PathPiecesImages = pathPiecesImages;
			// Inicializa los objetos
			GameBoardViewModel = new Board.GameBoardViewModel(this);
			PgnLibraryViewModel = new Pgn.PgnLibraryViewModel(GameBoardViewModel);
		}

		/// <summary>
		///		Carga un archivo
		/// </summary>
		public async Task<(bool loaded, string error)> LoadFileAsync()
		{
			return await PgnLibraryViewModel.LoadAsync(FileName);
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName 
		{
			get { return _fileName; }
			set { CheckProperty(ref _fileName, value); }
		}
		/// <summary>
		///		Librería de archivos PGN
		/// </summary>
		public Pgn.PgnLibraryViewModel PgnLibraryViewModel
		{
			get { return _pgnLibraryViewModel; }
			set { CheckObject(ref _pgnLibraryViewModel, value); }
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
