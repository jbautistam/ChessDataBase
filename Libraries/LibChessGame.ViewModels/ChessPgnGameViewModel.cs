﻿using System;
using System.Threading.Tasks;

namespace Bau.Libraries.LibChessGame.ViewModels
{
	/// <summary>
	///		ViewModel de la ventana principal
	/// </summary>
	public class ChessPgnGameViewModel : ChessGameBaseViewModel
	{
		// Variables privadas
		private string _fileName;
		private Pgn.PgnLibraryViewModel _pgnLibraryViewModel;

		public ChessPgnGameViewModel(string fileName, string pathBoardImages, string pathPiecesImages) : base(pathBoardImages, pathPiecesImages)
		{
			// Inicializa las propiedades
			FileName = fileName;
			// Inicializa los objetos
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
	}
}
