using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BauChessViewer.Controllers
{
	/// <summary>
	///		Controlador principal
	/// </summary>
	public class HostController : Bau.Libraries.LibChessGame.Mvvm.Controllers.IHostController
	{
		// Constantes privadas
		private const string SubPathBoard = @"Data\Graphics\Boards";
		private const string SubPathPiecess = @"Data\Graphics\Pieces";

		public HostController(string pathBase)
		{
			PathBase = pathBase;
		}

		/// <summary>
		///		Abre la ventana de selección de un archivo
		/// </summary>
		public async Task<string> OpenFileAsync(string pathBase, string filter, string defaultExt)
		{
			Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();

				// Utiliza await
				await Task.Delay(1);
				// Asigna las propiedades
				file.Multiselect = false;
				file.InitialDirectory = System.IO.Path.Combine(PathBase, "Data\\Samples");
				// file.FileName = defaultFileName;
				file.DefaultExt = "pgn";
				file.Filter = "Archivos PGN (*.pgn)|*.pgn|Todos los archivos|*.*";
				// Muestra el cuadro de diálogo y devuelve los archivos
				if (file.ShowDialog() ?? false && file.FileNames.Length > 0)
					return file.FileNames[0];
				else
					return string.Empty;
		}

		/// <summary>
		///		Obtiene un reader sobre un archivo
		/// </summary>
		public async Task<StreamReader> GetStreamReaderAsync(string fileName)
		{
			// Utiliza away
			await Task.Delay(1);
			// Devuelve el stream
			return new StreamReader(fileName);
		}

		/// <summary>
		///		Muestra un mensaje
		/// </summary>
		public async Task ShowMessageAsync(string message)
		{
			// Utiliza await
			await Task.Delay(1);
			// Muestra el mensaje
			System.Windows.MessageBox.Show(message);
		}

		/// <summary>
		///		Obtiene los directorios donde se encuentran las imágenes del tablero
		/// </summary>
		public async Task<List<string>> GetPathsBoardAsync()
		{
			return await GetSubPaths(Path.Combine(PathBase, SubPathBoard));
		}

		/// <summary>
		///		Obtiene los directorios donde se encuentran las imágenes de las piezas
		/// </summary>
		public async Task<List<string>> GetPathsPiecesAsync()
		{
			return await GetSubPaths(Path.Combine(PathBase, SubPathPiecess));
		}

		/// <summary>
		///		Obtiene los subdirectorios de un directorio
		/// </summary>
		private async Task<List<string>> GetSubPaths(string path)
		{
			List<string> directories = new List<string>();

				// Utiliza await
				await Task.Delay(1);
				// Obtiene los directorios hijo
				if (Directory.Exists(path))
					foreach (string directory in Directory.GetDirectories(path))
						directories.Add(directory);
				// Devuelve la lista de directorios
				return directories;
		}

		/// <summary>
		///		Directorio base de la aplicación
		/// </summary>
		public string PathBase { get; }
	}
}
