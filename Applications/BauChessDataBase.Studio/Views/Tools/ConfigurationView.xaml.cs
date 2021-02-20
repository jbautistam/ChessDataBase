using System;
using System.Windows;

using Bau.Libraries.ChessDataBase.ViewModels.Configuration;

namespace Bau.ChessDataBase.Studio.Views.Tools
{
	/// <summary>
	///		Vista para mostrar la configuración
	/// </summary>
	public partial class ConfigurationView : Window
	{
		public ConfigurationView(ConfigurationViewModel viewModel)
		{
			// Inicializa los componentes
			InitializeComponent();
			// Inicializa el viewModel
			DataContext = ViewModel = viewModel;
			ViewModel.Close += (sender, eventArgs) => 
									{
										DialogResult = eventArgs.IsAccepted; 
										Close();
									};
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		private ConfigurationViewModel ViewModel { get; }
	}
}
