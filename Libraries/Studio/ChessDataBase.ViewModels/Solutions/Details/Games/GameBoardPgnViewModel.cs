using System;

namespace Bau.Libraries.ChessDataBase.ViewModels.Solutions.Details.Games
{
	public class GameBoardPgnViewModel : BauMvvm.ViewModels.BaseObservableObject, Interfaces.IDetailViewModel
	{
		// Variables privadas
		private LibChessGame.ViewModels.ChessGameViewModel _chessGameViewModel;

		public GameBoardPgnViewModel(SolutionViewModel solutionViewModel, string fileName) : base(false)
		{
			// Asigna las propiedades
			SolutionViewModel = solutionViewModel;
			Header = System.IO.Path.GetFileName(fileName);
			ChessGameViewModel = new LibChessGame.ViewModels.ChessGameViewModel(fileName, 
																				SolutionViewModel.MainViewModel.ConfigurationViewModel.PathBoardImages,
																				SolutionViewModel.MainViewModel.ConfigurationViewModel.PathPiecesImages);
			// Añade el archivo a los últimos archivos abiertos
			SolutionViewModel.MainViewModel.ConfigurationViewModel.LastFilesViewModel.Add(fileName);
			// Asigna los comandos
			OpenFileCommand = new BauMvvm.ViewModels.BaseCommand(_ => OpenFile());
		}

		/// <summary>
		///		Abre el archivo en modo de texto
		/// </summary>
		private void OpenFile()
		{
			SolutionViewModel.MainViewModel.MainController.OpenWindow(new Files.FileViewModel(SolutionViewModel, ChessGameViewModel.FileName));
		}

		/// <summary>
		///		Obtiene el mensaje de grabar y cerrar
		/// </summary>
		public string GetSaveAndCloseMessage()
		{
			return string.Empty;
		}

		/// <summary>
		///		Graba el archivo
		/// </summary>
		public void SaveDetails(bool newName)
		{
			// No hace nada aún
		}

		/// <summary>
		///		ViewModel de la solución
		/// </summary>
		public SolutionViewModel SolutionViewModel { get; }

		/// <summary>
		///		Cabecera de la ficha
		/// </summary>
		public string Header { get; }

		/// <summary>
		///		ViewModel del juego
		/// </summary>
		public LibChessGame.ViewModels.ChessGameViewModel ChessGameViewModel
		{
			get { return _chessGameViewModel; }
			set { CheckObject(ref _chessGameViewModel, value); }
		}

		/// <summary>
		///		Id de la ficha
		/// </summary>
		public string TabId 
		{ 
			get { return $"PgnView_{ChessGameViewModel.FileName}"; }
		}

		/// <summary>
		///		Comando para abrir un archivo
		/// </summary>
		public BauMvvm.ViewModels.BaseCommand OpenFileCommand { get; }
	}
}
