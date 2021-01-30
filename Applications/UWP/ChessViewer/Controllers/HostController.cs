using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Storage;

namespace ChessViewer.Controllers
{
	/// <summary>
	///		Controlador principal
	/// </summary>
	public class HostController : Bau.Libraries.LibChessGame.Mvvm.Controllers.IHostController
	{
		// Constantes privadas
		internal const string SubPathBoard = @"Data\Graphics\Boards";
		internal const string SubPathPiecess = @"Data\Graphics\Pieces";

		public HostController(string pathBase)
		{
			PathBase = pathBase;
		}

		/// <summary>
		///		Abre la ventana de selección de un archivo
		/// </summary>
		public async Task<string> OpenFileAsync(string pathBase, string filter, string defaultExt)
		{
			Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
			StorageFile file;

				// Asigna las propiedades al cuadro de diálogo
				picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
				picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
				picker.FileTypeFilter.Add(defaultExt);
				// Obtiene el archivo
				file = await picker.PickSingleFileAsync();
				// Devuelve el nombre de archivo
				if (file != null)
				{
					// Añade el archivo a la lista de archivos más recientes
					Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(file, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
					// Añade el archivo a la lista de archivos accesibles
					Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFileToken", file);
					// Devuelve el nombre completo del archivo
					return file.Path;
				}
				else
					return string.Empty;
		}

		/// <summary>
		///		Obtiene el streamReader de un archivo
		/// </summary>
		public async Task<StreamReader> GetStreamReaderAsync(string fileName)
		{
			StorageFile file = await StorageFile.GetFileFromPathAsync(fileName);

				// Devuelve el stream
				return new StreamReader((await file.OpenReadAsync()).AsStreamForRead());
		}

		/// <summary>
		///		Muestra un mensaje
		/// </summary>
		public async Task ShowMessageAsync(string message)
		{
			await new MessageDialog(message).ShowAsync();
		}

		/// <summary>
		///		Obtiene los directorios donde se encuentran las imágenes del tablero
		/// </summary>
		public async Task<List<string>> GetPathsBoardAsync()
		{
			return await GetSubPathsAsync(SubPathBoard);
		}

		/// <summary>
		///		Obtiene los directorios donde se encuentran las imágenes de las piezas
		/// </summary>
		public async Task<List<string>> GetPathsPiecesAsync()
		{
			return await GetSubPathsAsync(SubPathPiecess);
		}

		/// <summary>
		///		Obtiene los subdirectorios de un directorio
		/// </summary>
		private async Task<List<string>> GetSubPathsAsync(string childPath)
		{
			StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(GetApplicationDataPath(childPath));
			// StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, path));
			List<string> directories = new List<string>();

				// Obtiene los directorios hijo
				foreach (StorageFolder folder in await localFolder?.GetFoldersAsync())
					directories.Add(folder.Path);
				// Devuelve la lista de directorios
				return directories;
		}

		/// <summary>
		///		Obtiene el directorio de datos de la aplicación
		/// </summary>
		private string GetApplicationDataPath(string path)
		{
			return Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, path);
		}
/*
 public async static Task<Folder> GetFolderInfoAsync(StorageFolder SelectFolder)
     {
         var FolderModel = new Folder();

         FolderModel.FolderName = SelectFolder.Name;
         IReadOnlyList<StorageFile> fileList = await SelectFolder?.GetFilesAsync();

         foreach (StorageFile file in fileList)
         {
             var subFile = new File();
             subFile.FileName = file.Name;
             FolderModel.SubFiles.Add(subFile);

         }
         IReadOnlyList<StorageFolder> folderList = await SelectFolder?.GetFoldersAsync();

         foreach (StorageFolder folder in folderList)
         {
             var subFolder = new Folder();
             subFolder.FolderName = folder.Name;
             FolderModel.SubFolders.Add(subFolder);

         }

         return FolderModel;
     }
*/
		

		/// <summary>
		///		Directorio base de la aplicación
		/// </summary>
		public string PathBase { get; }
	}
}
