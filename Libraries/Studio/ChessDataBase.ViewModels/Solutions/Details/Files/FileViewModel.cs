using System;
using System.Threading.Tasks;

using Bau.Libraries.BauMvvm.ViewModels;

namespace Bau.Libraries.ChessDataBase.ViewModels.Solutions.Details.Files
{
	/// <summary>
	///		ViewModel para un archivo
	/// </summary>
	public class FileViewModel : BaseObservableObject, Interfaces.IDetailViewModel
	{
		// Eventos públicos
		public event EventHandler<Controllers.EventArguments.EditorGoToLineEventArgs> GoToLineRequired;
		public event EventHandler<Controllers.EventArguments.EditorSelectedTextRequiredEventArgs> SelectedTextRequired;
		// Variables privadas
		private string _header, _fileName, _content;
		private System.Text.Encoding _fileEncoding;
		private bool _fileWithBom;

		public FileViewModel(SolutionViewModel solutionViewModel, string fileName)
		{
			SolutionViewModel = solutionViewModel;
			FileName = fileName;
		}

		/// <summary>
		///		Carga el texto del archivo
		/// </summary>
		public void Load()
		{
			// Carga el archivo
			if (!string.IsNullOrWhiteSpace(FileName) && System.IO.File.Exists(FileName))
			{
				// Obtiene la codificación del archivo (para grabarlo después con la misma codificación)
				FileEncoding = LibHelper.Files.HelperFiles.GetFileEncoding(FileName);
				if (FileEncoding == null)
				{
					FileEncoding = System.Text.Encoding.UTF8;
					FileWithBom = false;
				}
				else
					FileWithBom = true;
				// Carga el archivo
				Content = LibHelper.Files.HelperFiles.LoadTextFile(FileName);
			}
			// Añade el archivo a los últimos archivos abiertos
			SolutionViewModel.MainViewModel.ConfigurationViewModel.LastFilesViewModel.Add(FileName);
		}

		/// <summary>
		///		Graba el archivo
		/// </summary>
		public void SaveDetails(bool newName)
		{
			// Graba el archivo
			if (string.IsNullOrWhiteSpace(FileName) || newName)
			{
				string newFileName = SolutionViewModel.MainViewModel.OpenDialogSave(FileName, "Script SQL (*.sql)|*.sql|Todos los archivos (*.*)|*.*", ".sql");

					// Cambia el nombre de archivo si es necesario
					if (!string.IsNullOrWhiteSpace(newFileName))
						FileName = newFileName;
			}
			// Graba el archivo
			if (!string.IsNullOrWhiteSpace(FileName))
			{
				// Graba el archivo
				if (FileWithBom)
					LibHelper.Files.HelperFiles.SaveTextFile(FileName, Content, FileEncoding);
				else
					LibHelper.Files.HelperFiles.SaveTextFileWithoutBom(FileName, Content);
				// Actualiza el árbol
				SolutionViewModel.TreeFoldersViewModel.Load();
				// Añade el archivo a los últimos archivos abiertos
				SolutionViewModel.MainViewModel.ConfigurationViewModel.LastFilesViewModel.Add(FileName);
				// Indica que no ha habido modificaciones
				IsUpdated = false;
			}
		}

		/// <summary>
		///		Lanza un evento para colocar el editor en una línea
		/// </summary>
		internal void GoToLine(string textSelected, int line)
		{
			GoToLineRequired?.Invoke(this, new Controllers.EventArguments.EditorGoToLineEventArgs(textSelected, line));
		}

		/// <summary>
		///		Obtiene el mensaje que se debe mostrar al cerrar la ventana
		/// </summary>
		public string GetSaveAndCloseMessage()
		{
			if (string.IsNullOrWhiteSpace(FileName))
				return "¿Desea grabar el archivo antes de continuar?";
			else
				return $"¿Desea grabar el archivo '{System.IO.Path.GetFileName(FileName)}' antes de continuar?";
		}

		/// <summary>
		///		Solución
		/// </summary>
		public SolutionViewModel SolutionViewModel { get; }

		/// <summary>
		///		Id de la ficha
		/// </summary>
		public string TabId 
		{ 
			get { return GetType().ToString() + "_" + FileName; } 
		}

		/// <summary>
		///		Cabecera
		/// </summary>
		public string Header
		{
			get { return _header; }
			set { CheckProperty(ref _header, value); }
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
			set
			{ 
				if (CheckProperty(ref _fileName, value))
				{
					if (!string.IsNullOrWhiteSpace(value))
						Header = System.IO.Path.GetFileName(value);
					else
						Header = "Archivo";
				}
			}
		}

		/// <summary>
		///		Codificación del archivo
		/// </summary>
		public System.Text.Encoding FileEncoding
		{
			get { return _fileEncoding; }
			set { CheckObject(ref _fileEncoding, value); }
		}

		/// <summary>
		///		Indica si el archivo tiene una cabecera BOM
		/// </summary>
		public bool FileWithBom
		{
			get { return _fileWithBom; }
			set { CheckProperty(ref _fileWithBom, value); }
		}

		/// <summary>
		///		Contenido del archivo
		/// </summary>
		public string Content
		{
			get { return _content; }
			set { CheckProperty(ref _content, value); }
		}
	}
}
