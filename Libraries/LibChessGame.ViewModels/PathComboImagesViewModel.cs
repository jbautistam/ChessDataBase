using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Bau.Libraries.LibChessGame.ViewModels
{
	/// <summary>
	///		ViewModel para los combos de imágenes
	/// </summary>
	public class PathComboImagesViewModel : Mvvm.BaseObservableObject
	{
		// Enumerados públicos
		/// <summary>
		///		Tipo de directorio que contiene las imágenes
		/// </summary>
		public enum PathType
		{
			/// <summary>Imágenes de tablero</summary>
			Board,
			/// <summary>Imágenes de piezas</summary>
			Pieces
		}

		// Variables privadas
		private Dictionary<string, string> _dctPaths = new Dictionary<string, string>();
		private string _selectedPath;

		/// <summary>
		///		Inicializa los elementos del viewModel
		/// </summary>
		public async Task InitAsync(PathType type)
		{
			// Limpia los directorios
			Paths.Clear();
			_dctPaths.Clear();
			// Obtiene los directorios del controlador
			foreach (string path in await GetPathsAsync(type))
				if (System.IO.Directory.Exists(path))
				{
					string text = System.IO.Path.GetFileName(path);

						if (!_dctPaths.ContainsKey(text))
						{
							Paths.Add(text);
							_dctPaths.Add(text, path);
						}
				}
			// Selecciona el primer elemento
			if (Paths.Count > 0)
				SelectedPath = Paths[0];
		}

		/// <summary>
		///		Obtiene los directorios
		/// </summary>
		private async Task<List<string>> GetPathsAsync(PathType type)
		{
			switch (type)
			{
				case PathType.Board:
					return await MainViewModel.HostController.GetPathsBoardAsync();
				default:
					return await MainViewModel.HostController.GetPathsPiecesAsync();
			}
		}

		/// <summary>
		///		Obtiene el directorio completo
		/// </summary>
		public string GetFullPathName()
		{
			if (string.IsNullOrEmpty(SelectedPath) || !_dctPaths.ContainsKey(SelectedPath))
				return string.Empty;
			else
				return _dctPaths[SelectedPath];
		}

		/// <summary>
		///		Archivos
		/// </summary>
		public ObservableCollection<string> Paths { get; } = new ObservableCollection<string>();

		/// <summary>
		///		Nombre completo del directorio de imágenes
		/// </summary>
		public string SelectedPath
		{
			get { return _selectedPath; }
			set { CheckProperty(ref _selectedPath, value); }
		}
	}
}
