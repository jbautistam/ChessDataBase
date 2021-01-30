using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Bau.Libraries.LibChessGame.ViewModels.Board.Movements;

namespace ChessViewer.Views.Board.Templates
{
	public class MovementTemplateSelector : DataTemplateSelector
	{
		/// <summary>
		///		Selecciona la plantilla adecuada para un ViewModel
		/// </summary>
		protected override DataTemplate SelectTemplateCore(object item)
		{
			return GetDefinedTemplate(item);
		}

		/// <summary>
		///		Selecciona la plantilla adecuada para un ViewModel
		/// </summary>
		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{
			return GetDefinedTemplate(item);
		}

		/// <summary>
		///		Selecciona la plantilla adecuada para un ViewModel
		/// </summary>
		protected DataTemplate GetDefinedTemplate(object item)
		{
			switch (item)
			{
				case MovementFigureDoubleViewModel viewModel:
					return MovementFigureDoubleTemplate;
				case MovementGameEndViewModel viewModel:
					return MovementFigureEndGameTemplate;
				default:
					return MovementRemarksTemplate;
			}
		}
		
		/// <summary>
		///		Plantilla para los movimientos
		/// </summary>
		public DataTemplate MovementFigureDoubleTemplate { get; set; }

		/// <summary>
		///		Plantilla para el resultado del juego
		/// </summary>
		public DataTemplate MovementFigureEndGameTemplate { get; set; }

		/// <summary>
		///		Plantilla para comentarios
		/// </summary>
		public DataTemplate MovementRemarksTemplate { get; set; }
	}
}