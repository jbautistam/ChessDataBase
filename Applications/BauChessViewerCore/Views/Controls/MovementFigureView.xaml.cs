﻿using System;
using System.Windows;
using System.Windows.Controls;

using Bau.Libraries.LibChessGame.ViewModels.Board.Movements;

namespace Bau.ChessDataBase.Views.Controls
{
	/// <summary>
	///		Control que muestra el icono y el texto de un movimiento
	/// </summary>
	public partial class MovementFigureView : UserControl
	{
		// Propiedades
		public static readonly DependencyProperty MovementProperty = DependencyProperty.Register(nameof(Movement), typeof(MovementFigureViewModel),
																								 typeof(MovementFigureView), new PropertyMetadata(null));

		public MovementFigureView()
		{
			InitializeComponent();
			grdMovement.DataContext = this;
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		public MovementFigureViewModel Movement 
		{ 
			get { return (MovementFigureViewModel) GetValue(MovementProperty); }
			set { SetValue(MovementProperty, value); }
		}
	}
}
