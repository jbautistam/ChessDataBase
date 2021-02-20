using System;

using Bau.Libraries.BauMvvm.ViewModels;
using Bau.Libraries.ChessDataBase.Models;

namespace Bau.Libraries.ChessDataBase.ViewModels.Solutions
{
	/// <summary>
	///		ViewModel de la solución
	/// </summary>
	public class SolutionViewModel : BaseObservableObject
	{
		// Variables privadas
		private string _workspace;
		private Explorers.Files.TreeFilesViewModel _treeFoldersViewModel;

		public SolutionViewModel(MainViewModel mainViewModel)
		{
			// Asigna las propiedades
			MainViewModel = mainViewModel;
			// Asigna los árboles de exploración
			TreeFoldersViewModel = new Explorers.Files.TreeFilesViewModel(this);
			// Asigna los comandos
			NewWorkspaceCommand = new BaseCommand(_ => NewWorkspace());
			DeleteWorkspaceCommand = new BaseCommand(_ => DeleteWorkspace());
		}

		/// <summary>
		///		Carga un archivo de solución
		/// </summary>
		public void Load()
		{
			// Carga la solución
			Solution = MainViewModel.Manager.LoadConfiguration(MainViewModel.ConfigurationViewModel.Workspace);
			// Carga los exploradores
			TreeFoldersViewModel.Load();
			// Carga las carpetas en la ventana de búsqueda
			MainViewModel.SearchFilesViewModel.LoadFolders();
		}

		/// <summary>
		///		Crea un nuevo espacio de trabajo
		/// </summary>
		private void NewWorkspace()
		{
			string workspace = string.Empty;

				if (MainViewModel.MainController.HostController.SystemController.ShowInputString("Nombre del espacio de trabajo", ref workspace) == BauMvvm.ViewModels.Controllers.SystemControllerEnums.ResultType.Yes)
				{
					if (!string.IsNullOrWhiteSpace(workspace))
					{
						// Cambia el Workspace
						UpdateWorkspace(workspace);
						// Graba para que se cree el archivo
						MainViewModel.SaveSolution();
						// y lanza el evento de modificación
						MainViewModel.RaiseEventWorkSpaceChanged();
					}
				}
		}

		/// <summary>
		///		Modifica el espacio de trabajos
		/// </summary>
		public void UpdateWorkspace(string workspace)
		{
			// Cambia el espacio de trabajo
			MainViewModel.ConfigurationViewModel.Workspace = workspace;
			// Carga el espacio de trabajo
			Load();
		}

		/// <summary>
		///		Borra un espacio de trabajo
		/// </summary>
		private void DeleteWorkspace()
		{
			if (MainViewModel.MainController.HostController.SystemController.ShowQuestion($"¿Desea eliminar el espacio de trabajo '{MainViewModel.ConfigurationViewModel.Workspace}'?"))
			{
				// Borra el archivo
				MainViewModel.Manager.DeleteConfiguration(MainViewModel.ConfigurationViewModel.Workspace);
				// Pasa al workspace predeterminado
				UpdateWorkspace(MainViewModel.Manager.WorkSpace);
				// Lanza el evento de carga del menú
				MainViewModel.RaiseEventWorkSpaceChanged();
			}
		}

		/// <summary>
		///		ViewModel de la ventana principal
		/// </summary>
		public MainViewModel MainViewModel { get; }

		/// <summary>
		///		Solución
		/// </summary>
		public SolutionModel Solution { get; private set; }

		/// <summary>
		///		ViewModel del árbol de carpetas
		/// </summary>
		public Explorers.Files.TreeFilesViewModel TreeFoldersViewModel
		{
			get { return _treeFoldersViewModel; }
			set { CheckObject(ref _treeFoldersViewModel, value); }
		}

		/// <summary>
		///		Crea un nuevo espacio de trabajo
		/// </summary>
		public BaseCommand NewWorkspaceCommand { get; }

		/// <summary>
		///		Modifica el espacio de trabajo seleccionado
		/// </summary>
		public BaseCommand UpdateWorkspaceCommand { get; }

		/// <summary>
		///		Borra el espacio de trabajo seleccionado
		/// </summary>
		public BaseCommand DeleteWorkspaceCommand { get; }
	}
}