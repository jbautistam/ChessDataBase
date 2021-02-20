using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.ChessDataBase.Models;

namespace Bau.Libraries.ChessDataBase.Application.Repository
{
	/// <summary>
	///		Repository de <see cref="SolutionModel"/>
	/// </summary>
	internal class SolutionRepository
	{
		// Constantes privadas
		private const string TagRoot = "ChessDataBaseSolution";
		private const string TagName = "Name";
		private const string TagDescription = "Description";
		private const string TagId = "Id";
		private const string TagFolder = "Folder";

		/// <summary>
		///		Carga los datos de una solución
		/// </summary>
		internal SolutionModel Load(string fileName)
		{
			SolutionModel solution = new SolutionModel();
			MLFile fileML = new LibMarkupLanguage.Services.XML.XMLParser().Load(fileName);

				// Carga los datos de la solución
				if (fileML != null)
					foreach (MLNode rootML in fileML.Nodes)
						if (rootML.Name == TagRoot)
						{
							// Asigna las propiedades
							solution.FileName = fileName;
							solution.GlobalId = rootML.Attributes[TagId].Value;
							solution.Name = rootML.Nodes[TagName].Value.TrimIgnoreNull();
							solution.Description = rootML.Nodes[TagDescription].Value.TrimIgnoreNull();
							// Carga los objetos
							LoadFolders(solution, rootML);
						}
				// Devuelve la solución
				return solution;
		}

		/// <summary>
		///		Carga las carpetas de la solución
		/// </summary>
		private void LoadFolders(SolutionModel solution, MLNode rootML)
		{
			foreach (MLNode nodeML in rootML.Nodes)
				if (nodeML.Name == TagFolder)
					if (!string.IsNullOrWhiteSpace(nodeML.Value) && System.IO.Directory.Exists(nodeML.Value))
						solution.Folders.Add(nodeML.Value.TrimIgnoreNull());
		}

		/// <summary>
		///		Graba los datos de una solución
		/// </summary>
		internal void Save(SolutionModel solution, string fileName)
		{
			MLFile fileML = new MLFile();
			MLNode rootML = fileML.Nodes.Add(TagRoot);

				// Añade los datos de la solución
				rootML.Attributes.Add(TagId, solution.GlobalId);
				rootML.Nodes.Add(TagName, solution.Name);
				rootML.Nodes.Add(TagDescription, solution.Description);
				// Añade los objetos
				rootML.Nodes.AddRange(GetFoldersNodes(solution));
				// Graba el archivo
				new LibMarkupLanguage.Services.XML.XMLWriter().Save(fileName, fileML);
		}

		/// <summary>
		///		Obtiene los nodos con las carpetas de una solución
		/// </summary>
		private MLNodesCollection GetFoldersNodes(SolutionModel solution)
		{
			MLNodesCollection nodesML = new MLNodesCollection();

				// Añade los datos
				foreach (string folder in solution.Folders)
					nodesML.Add(TagFolder, folder);
				// Devuelve la colección de nodos
				return nodesML;
		}
	}
}