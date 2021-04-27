/*
 * $Id: ImportHandler.cs,v 1.2 2006/02/02 21:55:59 larsbm Exp $
 */

/*
 * License: 
 * GNU Lesser General Public License. You should recieve a
 * copy of this within the library. If not you will find
 * a whole copy at http://www.gnu.org/licenses/lgpl.html .
 * 
 * Author:
 * Copyright 2006, Lars Behrmann, lb@OpenDocument4all.com
 * 
 * Last changes:
 * 
 */

using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument;
using System;
using System.Collections;
using System.Diagnostics;

namespace AODL.Document.Import {
	/// <summary>
	/// ImportHandler class to get the right IImporter implementations
	/// for the document to import.
	/// </summary>
	public class ImportHandler {
		/// <summary>
		/// Initializes a new instance of the <see cref="ImportHandler"/> class.
		/// </summary>
		public ImportHandler () {
		}

		/// <summary>
		/// Gets the first importer that match the parameter criteria.
		/// </summary>
		/// <param name="documentType">Type of the document.</param>
		/// <param name="loadPath">The save path.</param>
		/// <returns></returns>
		public IImporter GetFirstImporter (DocumentTypes documentType, string loadPath) {
			// diub - Dipl.-Ing. Uwe Barth 2021-04-20
			string targetExtension = System.IO.Path.GetExtension(loadPath);

			foreach (IImporter iImporter in this.LoadImporter ()) {
				foreach (DocumentSupportInfo documentSupportInfo in iImporter.DocumentSupportInfos)
					if (documentSupportInfo.Extension.ToLower ().Equals (targetExtension.ToLower ()))
						if (documentSupportInfo.DocumentType == documentType)
							return iImporter;
			}

			AODLException exception     = new AODLException("No importer available for type "+documentType.ToString()+" and extension "+targetExtension);
			exception.InMethod = AODLException.GetExceptionSourceInfo (new StackFrame (1, true));
			throw exception;
		}

		/// <summary>
		/// Load internal and external importer.
		/// </summary>
		/// <returns></returns>
		private ArrayList LoadImporter () {
			try {
				ArrayList alImporter            = new ArrayList();
				alImporter.Add (new OpenDocumentImporter ());

				return alImporter;
			} catch (Exception e) {
				AODLException exception     = new AODLException("Error while trying to load the importer.");
				exception.InMethod = AODLException.GetExceptionSourceInfo (new StackFrame (1, true));
				exception.OriginalException = e;
				throw exception;
			}
		}
	}
}

/*
 * $Log: ImportHandler.cs,v $
 * Revision 1.2  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */