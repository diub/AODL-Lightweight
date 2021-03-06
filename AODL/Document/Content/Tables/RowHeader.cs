/*
 * $Id: RowHeader.cs,v 1.2 2006/02/05 20:02:25 larsbm Exp $
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

using AODL.Document.Styles;
using System.Xml;

namespace AODL.Document.Content.Tables {
	/// <summary>
	/// RowHeader represent a table row header.
	/// </summary>
	public class RowHeader : IContent {
		private Table _table;
		/// <summary>
		/// Gets or sets the table.
		/// </summary>
		/// <value>The table.</value>
		public Table Table {
			get {
				return this._table;
			}
			set {
				this._table = value;
			}
		}

		private RowCollection _rowCollection;
		/// <summary>
		/// Gets or sets the row collection.
		/// </summary>
		/// <value>The row collection.</value>
		public RowCollection RowCollection {
			get {
				return this._rowCollection;
			}
			set {
				this._rowCollection = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowHeader"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public RowHeader (IDocument document, XmlNode node) {
			this.Document = document;
			this.Node = node;
			this.InitStandards ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowHeader"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		public RowHeader (Table table) {
			this.Table = table;
			this.Document = table.Document;
			this.InitStandards ();
			//			this.RowCollection		= new RowCollection();
			this.NewXmlNode ();
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards () {
			this.RowCollection = new RowCollection ();

			this.RowCollection.Inserted += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (RowCollection_Inserted);
			this.RowCollection.Removed += new AODL.Document.Collections.CollectionWithEvents.CollectionChange (RowCollection_Removed);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode () {
			this.Node = this.Document.CreateNode ("table-header-rows", "table");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute (string name, string text, string prefix) {
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value = text;
			this.Node.Attributes.Append (xa);
		}

		#region IContent Member

		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName {
			get {
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set {
				XmlNode xn = this._node.SelectSingleNode("@table:style-name",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute ("style-name", value, "table");
				this._node.SelectSingleNode ("@table:style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		/// <value></value>
		public IDocument Document {
			get {
				return this._document;
			}
			set {
				this._document = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style {
			get {
				return this._style;
			}
			set {
				this.StyleName = value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node {
			get {
				return this._node;
			}
			set {
				this._node = value;
			}
		}

		#endregion

		/// <summary>
		/// Rows the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void RowCollection_Inserted (int index, object value) {
			this.Node.AppendChild (((Row) value).Node);
		}

		/// <summary>
		/// Rows the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void RowCollection_Removed (int index, object value) {
			this.Node.RemoveChild (((Row) value).Node);
		}

	}
}

/*
 * $Log: RowHeader.cs,v $
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 */